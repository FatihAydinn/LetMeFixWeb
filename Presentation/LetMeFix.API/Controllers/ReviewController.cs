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
            try
            {
                var values = await _repository.GetReviewsByJobId(jobId);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getProvidersReviews")]
        public async Task<IActionResult> GetProvidersReviews(string providerId)
        {
            try
            {
                var values = await _repository.GetReviewsByProvideId(providerId);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getCustomersReviews")]
        public async Task<IActionResult> GetCustomersReviews(string customerId)
        {
            try
            {
                var values = await _repository.GetReviewsByCustomerId(customerId);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getReviewById")]
        public async Task<IActionResult> GetReviewById(string reviewId)
        {
            try
            {
                var value = await _repository.GetByIdAsync(reviewId);
                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createReview")]
        public async Task<IActionResult> CreateReview([FromBody] Review review)
        {
            try
            {
                review.Id = Guid.NewGuid().ToString();
                await _repository.AddAsync(review);
                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateReview")]
        public async Task<IActionResult> UpdateReview([FromBody] Review review)
        {
            try
            {
                await _repository.UpdateAsync(review);
                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteReview")]
        public async Task<IActionResult> DeleteReview(string reviewId)
        {
            try
            {
                await _repository.DeleteAsync(reviewId);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
