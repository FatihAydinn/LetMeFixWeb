using LetMeFix.Application.Abstraction;
using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Persistence.Concretes
{
    public class TicketService : ITicketService
    {
        public List<Ticket> GetAllTickets() => new()
        {
            new Ticket() {Id = 1, Title = "Math lesson", Description = "Math lessons for my child", Price = 100, Date = DateTime.Now, Status = "Open"},
            new Ticket() {Id = 2, Title = "English lesson", Description = "English lessons for my child", Price = 200, Date = DateTime.Today, Status = "Processing"},
            new Ticket() {Id = 3, Title = "French lesson", Description = "French lessons for my child", Price = 300, Status = "Completed"},
        };
    }
}
