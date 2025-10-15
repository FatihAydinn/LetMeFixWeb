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
        private readonly IGenericRepository<Contracts> _contracts;

        public ContractsController(IGenericRepository<Contracts> contracts)
        {
            _contracts = contracts;
        }

        [HttpGet("getAllContracts")]
        public async Task<IActionResult> GetAllContracts()
        {
            try
            {
                var values = await _contracts.GetAllAsync();
                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getContractbyId")]
        public async Task<IActionResult> GetContractById(string id)
        {
            try
            {
                var value = await _contracts.GetByIdAsync(id);
                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createContract")]
        public async Task<IActionResult> CreateContract([FromBody] Contracts model)
        {
            try
            {
                model.Id = Guid.NewGuid().ToString();
                await _contracts.AddAsync(model);
                return Ok("success!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateContract")]
        public async Task<IActionResult> UpdateContract([FromBody] Contracts model)
        {
            try
            {
                await _contracts.UpdateAsync(model);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("cancelContract")]
        public async Task<IActionResult> CancelContract(string id)
        {
            try
            {
                Contracts model = await _contracts.GetByIdAsync(id);
                model.Status = JobStatus.Cancelled;


                await _contracts.UpdateAsync(model);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteContract")]
        public async Task<IActionResult> DeleteContract(string id)
        {
            try
            {
                await _contracts.DeleteAsync(id);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
