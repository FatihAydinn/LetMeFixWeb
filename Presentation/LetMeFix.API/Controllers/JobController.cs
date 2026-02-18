using LetMeFix.Domain.Interfaces;
using LetMeFix.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LetMeFix.Infrastructure.Services;
using MongoDB.Driver;
using LetMeFix.Application.Interfaces;
using LetMeFix.Application.Validations;
using LetMeFix.Application.DTOs;

namespace LetMeFix.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetJobs([FromQuery] PagedRequest request)
        {
            var filter = Builders<Job>.Filter.Where(x => true);
            var jobs = await _jobService.GetAllAsync(request);
            //var jobs = await _jobService.GetJobsPaged(filter, request);
            return Ok(jobs);
        }

        [HttpGet("get-byId")]
        public async Task<IActionResult> GetJobsById(string id)
        {
            var job = await _jobService.GetByIdAsync(id);
            return Ok(job);
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostJobs([FromBody] JobDTO job) {
            job.Id = Guid.NewGuid().ToString();
            await _jobService.AddAsync(job);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateJob(JobDTO job)
        {
            await _jobService.UpdateAsync(job);
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task DeleteJob(string id)
        {
            await _jobService.DeleteAsync(id);
        }

        [HttpGet("ListJobsPerUser")]
        public async Task<IActionResult> ListJobsPerUser(string userId, [FromQuery] PagedRequest request)
        {
            var filter = Builders<Job>.Filter.Eq(x => x.ProviderId, userId);
            var value = await _jobService.GetJobsPaged(filter, request);
            return Ok(value);
        }

        [HttpGet("ListJobsPerCategory")]
        public async Task<IActionResult> ListJobsPerCategory(string categoryId, [FromQuery] PagedRequest request)
        {
            var filter = Builders<Job>.Filter.Where(x => x.CategoryId.Length % 3 == 0 && x.CategoryId.Contains(categoryId));
            var value = await _jobService.GetJobsPaged(filter, request);
            return Ok(value);
        }

        [HttpGet("ListJobsPerCategoryByUser")]
        public async Task<IActionResult> ListJobsPerCategoryByUser(string categoryId, string userId, [FromQuery] PagedRequest request)
        {
            var filter = Builders<Job>.Filter.And(
                Builders<Job>.Filter.Eq(filter => filter.ProviderId, userId),
                Builders<Job>.Filter.Where(x => x.CategoryId.Length % 3 == 0 && x.CategoryId.Contains(categoryId))
            );
            var value = await _jobService.GetJobsPaged(filter, request);
            return Ok(value);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchJob(string search, [FromQuery] PagedRequest request)
        {
            var value = await _jobService.SearchJob(search, request);
            return Ok(value);
        }
    }
}
