using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using LetMeFix.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly ContractService _contracts;
        public ContractsController(ContractService contracts)
        {
            _contracts = contracts;
        }

        [HttpGet("getAllContracts")]
        public async Task<IActionResult> GetAllContracts([FromQuery] PagedRequest request)
        {
            var filter = Builders<Contracts>.Filter.Where(x => true);
            var values = await _contracts.GetPagedWithFilterAsync(filter, request);
            return Ok(values);
        }

        [HttpGet("getContractbyId")]
        public async Task<IActionResult> GetContractById(string id)
        {
            var value = await _contracts.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost("createContract")]
        public async Task<IActionResult> CreateContract([FromBody] Contracts model)
        {
            model.Id = Guid.NewGuid().ToString();
            await _contracts.AddAsync(model);
            return Ok("success!");
        }

        [HttpPut("updateContract")]
        public async Task<IActionResult> UpdateContract([FromBody] Contracts model)
        {
            await _contracts.UpdateAsync(model);
            return Ok("success");
        }

        [HttpDelete("deleteContract")]
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

        [HttpPut("changeStatus")]
        public async Task<IActionResult> ChangeStatus(string id, int status)
        {
            await _contracts.ChangeStatus(id, status);
            return Ok("success");
        }

        [HttpGet("getContractsByProviderId")]
        public async Task<IActionResult> GetContractsByProviderId([FromQuery] PagedRequest request, string userId)
        {
            var filter = Builders<Contracts>.Filter.Eq(x => x.ProviderId, userId);
            var result = await _contracts.GetPaged(filter, request);
            return Ok(result);
        }

        [HttpGet("getContractsByClientId")]
        public async Task<IActionResult> GetContractsByClientId([FromQuery] PagedRequest request, string userId)
        {
            var filter = Builders<Contracts>.Filter.Eq(x => x.ClientId, userId);
            var result = await _contracts.GetPaged(filter, request);
            return Ok(result);
        }

        [HttpGet("getContractByStatusAndUserId")]
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
            var result = await _contracts.GetPaged(filter, request);
            return Ok(result);
        }
    }
}
