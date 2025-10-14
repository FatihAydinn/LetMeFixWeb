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
            try
            {
                var value = await _offerRepository.GetByIdAsync(id);
                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createOffer")]
        public async Task<IActionResult> CreateOffer(Offers offer)
        {
            try
            {
                offer.Id = Guid.NewGuid().ToString();
                await _offerRepository.AddAsync(offer);
                return Ok(offer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateOffer")]
        public async Task<IActionResult> UpdateOffer(Offers offer)
        {
            try
            {
                await _offerRepository.UpdateAsync(offer);
                return Ok(offer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteOffer")]
        public async Task<IActionResult> DeleteOffer(string id)
        {
            try
            {
                await _offerRepository.DeleteAsync(id);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getOffersByJob")]
        public async Task<IActionResult> GetOffersByJob(string jobId)
        {
            try
            {
                var values = await _offerRepository.GetOffersByJobIdAsync(jobId);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getContractsByCustomerIdPerJob")]
        public async Task<IActionResult> GetContractsByCustomerIdPerJob(string jobId, string customerId)
        {
            try
            {
                var value = await _offerRepository.GetOffersByCustomerIPerJobId(jobId, customerId);
                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
