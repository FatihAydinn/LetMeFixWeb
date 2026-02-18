using LetMeFix.Application.DTOs;
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

        [HttpGet("get-all")]
        public async Task<PagedResult<OffersDTO>> GetAllOffers([FromQuery] PagedRequest request)
        {            
            return await _offer.GetAllAsync(request);
        }

        [HttpGet("get-byId")]
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateOffer(OffersDTO offer)
        {
            var newOffer = offer with { Id = Guid.NewGuid().ToString() };
            await _offer.CreateOfferAsync(newOffer);
            return Ok(newOffer);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateOffer(OffersDTO offer)
        {
            await _offer.UpdateAsync(offer);
            return Ok(offer);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteOffer(string id)
        {
            await _offer.DeleteAsync(id);
            return Ok("success");
        }

        [HttpGet("get-byJobId")]
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
