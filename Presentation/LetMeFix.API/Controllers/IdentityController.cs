using LetMeFix.Domain.DTOs;
using LetMeFix.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public IdentityController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
                string val = "";
                if (model.Email.Contains("@"))
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    val = user.UserName;
                }
                else val = model.UserName;

                var result = await _signInManager.PasswordSignInAsync(val, model.Password, false, false);

                if (result.Succeeded)
                {
                    return Ok("Login success!");
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
    }
}
