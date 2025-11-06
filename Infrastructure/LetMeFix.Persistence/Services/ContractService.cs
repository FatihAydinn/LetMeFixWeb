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
    public class ContractService : BaseService<Contracts>
    {
        public ContractService(IMongoDatabase contracts) : base (contracts, "Contracts")
        {
        }

        public async Task AddAsync(Contracts entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<Contracts>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<Contracts> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Contracts entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task GiveATip(string id, decimal tip)
        {
            var filter = Builders<Contracts>.Filter.Eq(x => x.Id, id);
            var update = Builders<Contracts>.Update.Set(x => x.Tip, tip);

            await _collection.UpdateOneAsync(filter, update);
        }
    }
}
