using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using LetMeFix.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _repository;
        public ReviewController(ReviewService repository)
        {
            _repository = repository;
        }

        [HttpGet("getJobReviews")]
        public async Task<IActionResult> getAllJobReviews(string jobId)
        {
            var values = await _repository.GetReviewsByJobId(jobId);
            return Ok(values);
        }

        [HttpGet("getProvidersReviews")]
        public async Task<IActionResult> GetProvidersReviews(string providerId)
        {
            var values = await _repository.GetReviewsByProvideId(providerId);
            return Ok(values);
        }

        [HttpGet("getCustomersReviews")]
        public async Task<IActionResult> GetCustomersReviews(string customerId)
        {
            var values = await _repository.GetReviewsByCustomerId(customerId);
            return Ok(values);
        }

        [HttpGet("getReviewById")]
        public async Task<IActionResult> GetReviewById(string reviewId)
        {
            var value = await _repository.GetByIdAsync(reviewId);
            return Ok(value);
        }

        [HttpPost("createReview")]
        public async Task<IActionResult> CreateReview([FromBody] Review review)
        {
            review.Id = Guid.NewGuid().ToString();
            await _repository.AddAsync(review);
            return Ok(review);
        }

        [HttpPut("updateReview")]
        public async Task<IActionResult> UpdateReview([FromBody] Review review)
        {
            await _repository.UpdateAsync(review);
            return Ok(review);
        }

        [HttpDelete("deleteReview")]
        public async Task<IActionResult> DeleteReview(string reviewId)
        {
            await _repository.DeleteAsync(reviewId);
            return Ok("success");
        }
    }
}
