using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly IOfferService _offer;
        public OffersController(IOfferService offer)
        {
            _offer = offer;
        }

        [HttpGet("getAllOffers")]
        public async Task<PagedResult<Offers>> GetAllOffers([FromQuery] PagedRequest request)
        {            
            return await _offer.GetAllAsync(request);
        }

        [HttpGet("getOfferById")]
        public async Task<IActionResult> GetOfferById(string id)
        {
            var value = await _offer.GetByIdAsync(id);
            return Ok(value);
        }

        //[HttpPost("createOffer")]
        //public async Task<IActionResult> CreateOffer(Offers offer)
        //{
        //    offer.Id = Guid.NewGuid().ToString();
        //    await _repository.AddAsync(offer);
        //    return Ok(offer);
        //}

        [HttpPost("createOffer")]
        public async Task<IActionResult> CreateOffer(Offers offer)
        {
            offer.Id = Guid.NewGuid().ToString();
            await _offer.CreateOfferAsync(offer);
            return Ok(offer);
        }

        [HttpPut("updateOffer")]
        public async Task<IActionResult> UpdateOffer(Offers offer)
        {
            await _offer.UpdateAsync(offer);
            return Ok(offer);
        }

        [HttpDelete("deleteOffer")]
        public async Task<IActionResult> DeleteOffer(string id)
        {
            await _offer.DeleteAsync(id);
            return Ok("success");
        }

        [HttpGet("getOffersByJob")]
        public async Task<IActionResult> GetOffersByJob([FromQuery] PagedRequest request, string jobId)
        {
            var values = await _offer.GetOffersByJobIdAsync(request, jobId);
            return Ok(values);
        }

        [HttpGet("getContractsByCustomerIdPerJob")]
        public async Task<IActionResult> GetContractsByCustomerIdPerJob([FromQuery] PagedRequest request, string jobId, string customerId)
        {
            var value = await _offer.GetOffersByCustomerIPerJobId(request, jobId, customerId);
            return Ok(value);
        }
    }
}
