using LetMeFix.Domain.Interfaces;
using LetMeFix.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IGenericRepository<Job> _genericService;
        public JobController(IGenericRepository<Job> jobService)
        {
            _genericService = jobService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetJobs()
        {
            var jobs = await _genericService.GetAllAsync();
            return Ok(jobs);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetJobsById(string id)
        {
            var job = await _genericService.GetByIdAsync(id);
            return Ok(job);
        }

        [HttpPost("AddJob")]
        public async Task<IActionResult> PostJobs([FromBody] Job job) {
            job.Id = Guid.NewGuid().ToString();
            await _genericService.AddAsync(job);
            return Ok();
            //return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);
        }

        [HttpPost("UpdateJob")]
        public async Task<IActionResult> UpdateJob(Job job)
        {
            await _genericService.UpdateAsync(job);
            return Ok();
        }

        [HttpDelete("DeleteJob")]
        public async Task DeleteJob(string id)
        {
            await _genericService.DeleteAsync(id);
        }
    }
}
