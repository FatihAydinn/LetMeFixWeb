using LetMeFix.Application.Abstraction;
using LetMeFix.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IGenericRepository<Ticket> _genericService;
        public TicketsController(IGenericRepository<Ticket> ticketService)
        {
            _genericService = ticketService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _genericService.GetAllAsync();
            return Ok(tickets);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetTicketsById(string id)
        {
            var ticket = await _genericService.GetByIdAsync(id);
            return Ok(ticket);
        }

        [HttpPost("AddTicket")]
        public async Task<IActionResult> PostTickets([FromBody] Ticket ticket) {
            await _genericService.AddAsync(ticket);
            return Ok();
            //return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
        }

        [HttpPost("UpdateTicket")]
        public async Task<IActionResult> UpdateTicket(Ticket ticket)
        {
            await _genericService.UpdateAsync(ticket);
            return Ok();
        }

        [HttpDelete("DeleteTicket")]
        public async Task DeleteTicket(string id)
        {
            await _genericService.DeleteAsync(id);
        }
    }
}
