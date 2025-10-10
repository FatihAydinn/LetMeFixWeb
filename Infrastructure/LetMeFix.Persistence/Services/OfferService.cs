using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Persistence.Services
{
    public class OfferService : IGenericRepository<Offers>
    {
        private readonly IMongoCollection<Offers> _collection;

        public OfferService(IMongoDatabase database)
        {
            _collection = database.GetCollection<Offers>("Offers");
        }

        public async Task AddAsync(Offers entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<Offers>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();    
        }

        public async Task<Offers> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Offers> GetByCustomerIdandJobIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Offers entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }
    }
}
