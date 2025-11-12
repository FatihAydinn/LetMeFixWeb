using LetMeFix.Application.DTOs;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using LetMeFix.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInformationsController : ControllerBase
    {
        //private readonly IGenericRepository<UserInformations> _userService;
        private readonly UserInformationService _userService;
        public UserInformationsController(UserInformationService userService)
        {
            _userService = userService;
        }

        [HttpGet("getUserById")]
        public async Task<IActionResult> GetUserById (string id)
        {
            var userval = await _userService.GetByIdAsync(id);
            return Ok(userval);
        }

        [HttpPost("createUser")]
        public async Task<IActionResult> CreateNewUser([FromBody] UserInformations user)
        {
            //user.Id = Guid.NewGuid().ToString();
            await _userService.AddAsync(user);
            return Ok(user);
        }
        
        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserInformations user)
        {
            await _userService.UpdateAsync(user);
            return Ok(user);
        }

        [HttpPut("updateSocials")]
        public async Task<IActionResult> UpdateSocials([FromBody] UserInformationSocialsDTO user)
        {
            await _userService.UpdateSocials(user);
            return Ok(user);
        }

        [HttpPut("updateAddress")]
        public async Task<IActionResult> UpdateAddress([FromBody] UserinformationAddressDTO user)
        {
            await _userService.UpdateAddress(user);
            return Ok(user);
        }
    }
}
