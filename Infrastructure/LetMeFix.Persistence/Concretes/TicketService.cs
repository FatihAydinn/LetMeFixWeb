using LetMeFix.Application.Abstraction;
using LetMeFix.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Persistence.Concretes
{
    public class TicketService : ITicketRepository
    {
        //public List<Ticket> GetAllTickets() => new()
        //{
        //    new Ticket() {Id = 1, Title = "Math lesson", Description = "Math lessons for my child", Price = 100, Date = DateTime.Now, Status = "Open"},
        //    new Ticket() {Id = 2, Title = "English lesson", Description = "English lessons for my child", Price = 200, Date = DateTime.Today, Status = "Processing"},
        //    new Ticket() {Id = 3, Title = "French lesson", Description = "French lessons for my child", Price = 300, Status = "Completed"},
        //};

        private readonly IMongoCollection<Ticket> _collection;

        public TicketService(IMongoDatabase database)
        {
            _collection = database.GetCollection<Ticket>("Tickets");
        }

        public async Task AddAsync(Ticket ticket)
        {
            await _collection.InsertOneAsync(ticket);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public Task<Ticket> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
