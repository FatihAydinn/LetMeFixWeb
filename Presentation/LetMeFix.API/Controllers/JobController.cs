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
        public async Task<IActionResult> GetJobs(int page = 1, int pageSize = 2)
        {
            var jobs = await _jobService.GetJobsPaged(page, pageSize);
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

        [HttpGet("ListJobsPerUser")]
        public async Task<IActionResult> ListJobsPerUser(string userId)
        {
            var value = await _jobService.ListJobsPerUser(userId);
            return Ok(value);
        }

        [HttpGet("ListJobsPerCategory")]
        public async Task<IActionResult> ListJobsPerCategory(string categoryId)
        {
            var value = await _jobService.ListJobsPerCategory(categoryId);
            return Ok(value);
        }

        [HttpGet("searchJob")]
        public async Task<IActionResult> SearchJob(string search)
        {
            var value = await _jobService.SearchJob(search);
            return Ok(value);
        }
    }
}
