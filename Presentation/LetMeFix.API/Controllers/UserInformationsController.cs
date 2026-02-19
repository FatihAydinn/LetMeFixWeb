using LetMeFix.Application.DTOs;
using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInformationsController : ControllerBase
    {
        private readonly IUserInformationService _userService;
        public UserInformationsController(IUserInformationService userService)
        {
            _userService = userService;
        }

        //admin
        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromQuery] PagedRequest request)
        {
            var result = await _userService.GetAllAsync(request);
            return Ok(result);
        }

        [HttpGet("getUserById")]
        public async Task<IActionResult> GetUserById (string id)
        {
            var userval = await _userService.GetByIdAsync(id);
            return Ok(userval);
        }

        [HttpPost("createUser")]
        public async Task<IActionResult> CreateNewUser([FromBody] UserInformationsDTO user)
        {
            var newUser = user with { Id = Guid.NewGuid().ToString() };
            await _userService.AddAsync(newUser);
            return Ok(newUser);
        }
        
        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserInformationsDTO user)
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

        [HttpPut("updateSummary")]
        public async Task<IActionResult> UpdateSummary([FromBody] UserInformationSummaryDTO user)
        {
            await _userService.UpdateSummary(user);
            return Ok(user);
        }
    }
}
