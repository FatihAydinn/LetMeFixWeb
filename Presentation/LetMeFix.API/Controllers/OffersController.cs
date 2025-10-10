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
        private readonly IGenericRepository<Offers> _offers;

        public OffersController(IGenericRepository<Offers> offers)
        {
            _offers = offers;
        }

        [HttpGet("getOfferById")]
        public async Task<IActionResult> GetOfferById(string id)
        {
            try
            {
                var value = await _offers.GetByIdAsync(id);
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
                await _offers.AddAsync(offer);
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
                await _offers.UpdateAsync(offer);
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
                await _offers.DeleteAsync(id);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
