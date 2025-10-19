using LetMeFix.Domain.Interfaces;
using LetMeFix.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LetMeFix.Infrastructure.Services;

namespace LetMeFix.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly JobService _jobService;
        public JobController(JobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetJobs()
        {
            var jobs = await _jobService.GetAllAsync();
            return Ok(jobs);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetJobsById(string id)
        {
            var job = await _jobService.GetByIdAsync(id);
            return Ok(job);
        }

        [HttpPost("AddJob")]
        public async Task<IActionResult> PostJobs([FromBody] Job job) {
            job.Id = Guid.NewGuid().ToString();
            await _jobService.AddAsync(job);
            return Ok();
            //return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);
        }

        [HttpPut("UpdateJob")]
        public async Task<IActionResult> UpdateJob(Job job)
        {
            await _jobService.UpdateAsync(job);
            return Ok();
        }

        [HttpDelete("DeleteJob")]
        public async Task DeleteJob(string id)
        {
            await _jobService.DeleteAsync(id);
        }
    }
}
