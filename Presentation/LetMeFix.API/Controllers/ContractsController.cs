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
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contracts;

        public ContractsController(IContractService contracts)
        {
            _contracts = contracts;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllContracts([FromQuery] PagedRequest request)
        {
            var filter = Builders<Contracts>.Filter.Where(x => true);
            var values = await _contracts.GetPagedWithFilterAsync(request, filter);
            return Ok(values);
        }

        [HttpGet("get-byId")]
        public async Task<IActionResult> GetContractById(string id)
        {
            var value = await _contracts.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateContract([FromBody] ContractsDTO model)
        {
            model.Id = Guid.NewGuid().ToString();
            await _contracts.AddAsync(model);
            return Ok("success!");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateContract([FromBody] ContractsDTO model)
        {
            await _contracts.UpdateAsync(model);
            return Ok("success");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteContract(string id)
        {
            await _contracts.DeleteAsync(id);
            return Ok("success");
        }

        [HttpPut("giveATip")]
        public async Task<IActionResult> GiveATip(string id, decimal tip)
        {
            await _contracts.GiveATip(id, tip);
            return Ok(tip);
        }

        [HttpPut("change-status")]
        public async Task<IActionResult> ChangeStatus(string id, int status)
        {
            await _contracts.ChangeStatus(id, status);
            return Ok("success");
        }

        [HttpGet("get-byProviderId")]
        public async Task<IActionResult> GetContractsByProviderId([FromQuery] PagedRequest request, string userId)
        {
            var filter = Builders<Contracts>.Filter.Eq(x => x.ProviderId, userId);
            var result = await _contracts.GetPagedWithFilterAsync(request, filter);
            return Ok(result);
        }

        [HttpGet("get-byClientId")]
        public async Task<IActionResult> GetContractsByClientId([FromQuery] PagedRequest request, string userId)
        {
            var filter = Builders<Contracts>.Filter.Eq(x => x.ClientId, userId);
            var result = await _contracts.GetPagedWithFilterAsync(request, filter);
            return Ok(result);
        }

        [HttpGet("get-byStatusAndUserId")]
        public async Task<IActionResult> GetContractByStatusAndUserId([FromQuery] PagedRequest request, string userId, int status)
        {
            var enumStatus = (JobStatus)status;
            var userfilter = Builders<Contracts>.Filter.Or(
                         Builders<Contracts>.Filter.Eq(x => x.ProviderId, userId),
                         Builders<Contracts>.Filter.Eq(x => x.ClientId, userId)
            );
            var filter = Builders<Contracts>.Filter.And(
                         userfilter,
                         Builders<Contracts>.Filter.Eq(x => x.Status, enumStatus)
            );
            var result = await _contracts.GetPagedWithFilterAsync(request, filter);
            return Ok(result);
        }
    }
}
