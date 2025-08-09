using LetMeFix.Application.Abstraction;
using LetMeFix.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository _ticketService;
        public TicketsController(ITicketRepository ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _ticketService.GetAllAsync();
            return Ok(tickets);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetTicketsById(string id)
        {
            var ticket = await _ticketService.GetByIdAsync(id);
            return Ok(ticket);
        }

        [HttpPost("AddTicket")]
        public async Task<IActionResult> PostTickets([FromBody] Ticket ticket) {
            await _ticketService.AddAsync(ticket);
            return Ok();
            //return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
        }

        [HttpPost("UpdateTicket")]
        public async Task<IActionResult> UpdateTicket(Ticket ticket)
        {
            await _ticketService.UpdateAsync(ticket);
            return Ok();
        }

        [HttpDelete("DeleteTicket")]
        public async Task DeleteTicket(string id)
        {
            await _ticketService.DeleteAsync(id);
        }
    }
}
