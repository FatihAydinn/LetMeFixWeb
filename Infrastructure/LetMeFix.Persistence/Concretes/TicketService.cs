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
        private readonly IMongoCollection<Ticket> _collection;

        public TicketService(IMongoDatabase database)
        {
            _collection = database.GetCollection<Ticket>("Tickets");
        }

        public async Task AddAsync(Ticket ticket)
        {
            await _collection.InsertOneAsync(ticket);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(t=>t.Id == id);
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<Ticket> GetByIdAsync(string id)
        {
            return await _collection.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            await _collection.ReplaceOneAsync(t => t.Id == ticket.Id, ticket);
        }
    }
}
