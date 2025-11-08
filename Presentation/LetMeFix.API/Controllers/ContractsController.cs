using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using LetMeFix.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllContracts()
        {
            var values = await _contracts.GetAllAsync();
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
    }
}
