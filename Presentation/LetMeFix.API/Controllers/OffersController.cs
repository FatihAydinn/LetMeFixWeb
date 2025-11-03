using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using LetMeFix.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly OfferService _offerRepository;
        public OffersController(OfferService offerRepository)
        {
            _offerRepository = offerRepository;
        }

        [HttpGet("getOfferById")]
        public async Task<IActionResult> GetOfferById(string id)
        {
            var value = await _offerRepository.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost("createOffer")]
        public async Task<IActionResult> CreateOffer(Offers offer)
        {
            offer.Id = Guid.NewGuid().ToString();
            await _offerRepository.AddAsync(offer);
            return Ok(offer);
        }

        [HttpPut("updateOffer")]
        public async Task<IActionResult> UpdateOffer(Offers offer)
        {
            await _offerRepository.UpdateAsync(offer);
            return Ok(offer);
        }

        [HttpDelete("deleteOffer")]
        public async Task<IActionResult> DeleteOffer(string id)
        {
            await _offerRepository.DeleteAsync(id);
            return Ok("success");
        }

        [HttpGet("getOffersByJob")]
        public async Task<IActionResult> GetOffersByJob(string jobId)
        {
            var values = await _offerRepository.GetOffersByJobIdAsync(jobId);
            return Ok(values);
        }

        [HttpGet("getContractsByCustomerIdPerJob")]
        public async Task<IActionResult> GetContractsByCustomerIdPerJob(string jobId, string customerId)
        {
            var value = await _offerRepository.GetOffersByCustomerIPerJobId(jobId, customerId);
            return Ok(value);
        }
    }
}
