using LetMeFix.Infrastructure.Services;
using LetMeFix.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetMeFix.Domain.Interfaces;

namespace LetMeFix.Infrastructure.Services
{
    public class JobService : IGenericRepository<Job>
    {
        private readonly IMongoCollection<Job> _collection;

        public JobService(IMongoDatabase database)
        {
            _collection = database.GetCollection<Job>("Tickets");
        }

        public async Task AddAsync(Job ticket)
        {
            await _collection.InsertOneAsync(ticket);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(t => t.Id == id);
        }

        public async Task<List<Job>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<Job> GetByIdAsync(string id)
        {
            return await _collection.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Job ticket)
        {
            await _collection.ReplaceOneAsync(t => t.Id == ticket.Id, ticket);
        }
    }
}
