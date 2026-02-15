using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewRepository;
        public ReviewController(IReviewService reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        //admin
        [HttpGet("getAllReviews")]
        public async Task<IActionResult> getAllReviews([FromQuery] PagedRequest request)
        {
            var filter = Builders<Review>.Filter.Where(x => true);
            var values = await _reviewRepository.GetPagedWithFilterAsync(request, filter);
            return Ok(values);
        }

        [HttpGet("getReviewsByUserId")]
        public async Task<IActionResult> GetReviewsByUserId([FromQuery] PagedRequest request, string userId)
        {
            var values = await _reviewRepository.GetReviewsByUserId(request, userId);
            return Ok(values);
        }

        [HttpGet("getReviewsByJobId")]
        public async Task<IActionResult> GetReviewsByJobId([FromQuery] PagedRequest request, string jobId)
        {
            var values = await _reviewRepository.GetReviewsByJobId(request, jobId);
            return Ok(values);
        }

        [HttpGet("getReviewById")]
        public async Task<IActionResult> GetReviewById(string reviewId)
        {
            var value = await _reviewRepository.GetByIdAsync(reviewId);
            return Ok(value);
        }

        [HttpPost("createReview")]
        public async Task<IActionResult> CreateReview([FromBody] Review review)
        {
            review.Id = Guid.NewGuid().ToString();
            await _reviewRepository.AddAsync(review);
            return Ok(review);
        }

        [HttpPut("updateReview")]
        public async Task<IActionResult> UpdateReview([FromBody] Review review)
        {
            await _reviewRepository.UpdateAsync(review);
            return Ok(review);
        }

        [HttpDelete("deleteReview")]
        public async Task<IActionResult> DeleteReview(string reviewId)
        {
            await _reviewRepository.DeleteAsync(reviewId);
            return Ok("success");
        }
    }
}
