using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedJobsController : ControllerBase
    {
        private readonly ISavedJobService _savedJobs;

        public SavedJobsController(ISavedJobService savedJobs)
        {
            _savedJobs = savedJobs;
        }

        [HttpPost("saveAJob")]
        public async Task<IActionResult> SaveAJob(SavedJobs job)
        {
            await _savedJobs.AddAsync(job);
            return Ok(job);
        }

        [HttpGet("getSavedJobsByUserId")]
        public async Task<PagedResult<SavedJobs>> GetSavedJobsByUserId(PagedRequest request, string userId) {
            var value = await _savedJobs.GetSavedJobsByUserId(request, userId);
            return value;
        }

        [HttpDelete("deleteSavedJob")]
        public async Task<IActionResult> DeleteSavedJob(string jobId) {
            await _savedJobs.DeleteAsync(jobId);
            return Ok("success");
        }
    }
}
