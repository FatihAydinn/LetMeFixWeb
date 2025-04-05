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

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _ticketService.GetAllAsync();
            return Ok(tickets);
        }

        [HttpPost]
        public async Task<IActionResult> PostTickets([FromBody] Ticket ticket) {
            await _ticketService.AddAsync(ticket);
            return Ok();
            //return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
        }
    }
}
