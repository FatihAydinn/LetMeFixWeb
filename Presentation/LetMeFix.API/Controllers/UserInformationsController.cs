using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInformationsController : ControllerBase
    {
        private readonly IGenericRepository<UserInformations> _userService;
        public UserInformationsController(IGenericRepository<UserInformations> userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> GetUserById (string id)
        {
            try
            {
                var userval = await _userService.GetByIdAsync(id);
                return Ok(userval);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> UpdateUser([FromBody] UserInformations user)
        {
            try
            {
                await _userService.UpdateAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> CreateNewUser([FromBody] UserInformations user)
        {
            try
            {
                await _userService.AddAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
