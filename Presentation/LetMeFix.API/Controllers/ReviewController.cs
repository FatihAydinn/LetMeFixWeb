using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using LetMeFix.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

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

        //admin
        [HttpGet("getAllReviews")]
        public async Task<IActionResult> getAllReviews([FromQuery] PagedRequest request)
        {
            var filter = Builders<Review>.Filter.Where(x => true);
            var values = await _repository.GetPagedWithFilterAsync(filter, request);
            return Ok(values);
        }

        [HttpGet("getJobsReviews")]
        public async Task<IActionResult> GetJobsReviews([FromQuery] PagedRequest request, string jobId)
        {
            var filter = Builders<Review>.Filter.Eq(x => x.JobId, jobId);
            var value = await _repository.GetJobReviewsPaged(filter, request);
            return Ok(value);
        }

        //users own reviews
        [HttpGet("getProvidersReviews")]
        public async Task<IActionResult> GetProvidersReviews(string providerId, [FromQuery] PagedRequest request)
        {
            var filter = Builders<Review>.Filter.Eq(x => x.ProviderId, providerId);
            var values = await _repository.GetPagedWithFilterAsync(filter, request);
            return Ok(values);
        }

        //users received reviews
        [HttpGet("getCustomersReviews")]
        public async Task<IActionResult> GetCustomersReviews(string customerId, [FromQuery] PagedRequest request)
        {
            var filter = Builders<Review>.Filter.Eq(x => x.CustomerId, customerId);
            var values = await _repository.GetPagedWithFilterAsync(filter, request);
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
