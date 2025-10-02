using LetMeFix.Domain.Interfaces;
using LetMeFix.Application.DTOs;
using LetMeFix.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _jwtService;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtService jwtService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            try
            {
                var user = new AppUser { Name = model.Name, LastName = model.Lastname, UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded) {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Errors);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto model)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId)) return BadRequest("Invalid User");

                var result = await _jwtService.RevokeRefreshToken(userId);
                if (!result) return BadRequest("Logout failed!");

                Response.Cookies.Delete("access_token");
                Response.Cookies.Delete("refresh_token");

                return Ok(new { message = "Logout successfull!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
    }
}
