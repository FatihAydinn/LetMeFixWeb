using LetMeFix.Domain.Interfaces;
using LetMeFix.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LetMeFix.Infrastructure.Services;
using MongoDB.Driver;
using LetMeFix.Application.Interfaces;
using LetMeFix.Application.Validations;

namespace LetMeFix.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IGenericRepository<Job> _genericRepository;
        public JobController(IJobService jobService, IGenericRepository<Job> genericRepository)
        {
            _jobService = jobService;
            _genericRepository = genericRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetJobs([FromQuery] PagedRequest request)
        {
            var filter = Builders<Job>.Filter.Where(x => true);
            var jobs = await _jobService.GetAllAsync(request);
            //var jobs = await _jobService.GetJobsPaged(filter, request);
            return Ok(jobs);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetJobsById(string id)
        {
            var job = await _genericRepository.GetByIdAsync(id);
            return Ok(job);
        }

        [HttpPost("AddJob")]
        public async Task<IActionResult> PostJobs([FromBody] Job job) {
            job.Id = Guid.NewGuid().ToString();
            await _genericRepository.AddAsync(job);
            return Ok();
        }

        [HttpPut("UpdateJob")]
        public async Task<IActionResult> UpdateJob(Job job)
        {
            await _genericRepository.UpdateAsync(job);
            return Ok();
        }

        [HttpDelete("DeleteJob")]
        public async Task DeleteJob(string id)
        {
            await _genericRepository.DeleteAsync(id);
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

        [HttpGet("searchJob")]
        public async Task<IActionResult> SearchJob(string search, [FromQuery] PagedRequest request)
        {
            var value = await _jobService.SearchJob(search, request);
            return Ok(value);
        }
    }
}
