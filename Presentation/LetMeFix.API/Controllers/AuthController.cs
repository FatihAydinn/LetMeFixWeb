using LetMeFix.Domain.Interfaces;
using LetMeFix.Application.DTOs;
using LetMeFix.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LetMeFix.Application.Mappings;
using AutoMapper;
using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Caching.Memory;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtService jwtService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            //var user = new AppUser { Name = model.Name, LastName = model.Lastname, UserName = model.UserName, Email =model.Email };
            //var result = await _userManager.CreateAsync(user, model.Password);

            var user = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) {
                await GenerateConfirmationLink(user);
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            AppUser user = null;

            if (model.Email.Contains("@")) user = await _userManager.FindByEmailAsync(model.Email);
            else user = await _userManager.FindByNameAsync(model.UserName);

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            var token = await _jwtService.GenerateTokenAsync(user);

            //refresh token
            var refreshToken = _jwtService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                Response.Cookies.Append("access_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    //for prod = true
                    Secure = false, 
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(15)
                });

                Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    //
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(7)
                });


                return Ok(new
                {
                    token,
                    refreshToken,
                    //expiration = DateTime.UtcNow.AddMinutes(15),
                    user = new { user.Id, user.Email, user.UserName }
                });
                //return Ok(new
                //{
                //    token = token,
                //    expiration = DateTime.UtcNow.AddMinutes(60),
                //    user = new { user.Id, user.Email, user.UserName }
                //});
            }
            else
            {
                return BadRequest("Username or password incorrect!");
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return Unauthorized("Invalid refresh token");

            var newToken = await _jwtService.GenerateTokenAsync(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            Response.Cookies.Append("access_token", newToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });

            return Ok(new
            {
                token = newToken,
                refreshToken = newRefreshToken,
                //expiration = DateTime.UtcNow.AddMinutes(15)
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId)) return BadRequest("Invalid User");

            var result = await _jwtService.RevokeRefreshToken(userId);
            if (!result) return BadRequest("Logout failed!");

            Response.Cookies.Delete("access_token");
            Response.Cookies.Delete("refresh_token");

            return Ok(new { message = "Logout successfull!" });
        }

        [Authorize] 
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(new
            {
                user.Id,
                user.Email,
                user.UserName,
                user.Name,
                user.LastName
            });
        }

        [NonAction]
        public async Task GenerateConfirmationLink(AppUser model)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(model);
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var link = $"{baseUrl}/api/Auth/confirm-email?userId={model.Id}&token={WebUtility.UrlEncode(token)}";
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return Ok(result.Succeeded? "success": "failed");
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(string userId, string oldPassword, string newPassword, string confirmNewPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (newPassword != confirmNewPassword) return BadRequest("passwords are not matched");

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded) return Ok("password changed successfully");
            else return BadRequest(result.Errors);
        }

        [HttpGet("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest("email not found!");
            Random random = new Random();
            int code = random.Next(999, 9999);
            _cache.Set($"reset{email}", code, TimeSpan.FromMinutes(10));
            return Ok("Code sent!");
        }

        [HttpGet("verify-resetpassword-code")]
        public async Task<IActionResult> VerifyResetpasswordCode(string email, int code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var cachecode = _cache.Get<int>($"reset{email}");
            if (code != cachecode) return BadRequest("codes not match!");
            
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            _cache.Set($"resetToken{user.Id}", resetToken, TimeSpan.FromMinutes(10));
            var userId = user.Id;
            return Ok();
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword(string userid, string password)
        {
            var user = await _userManager.FindByIdAsync(userid);
            var token = _cache.Get<string>($"resetToken{user.Id}");
            await _userManager.ResetPasswordAsync(user, token, password);
            return Ok("success");
        }
    }
}
