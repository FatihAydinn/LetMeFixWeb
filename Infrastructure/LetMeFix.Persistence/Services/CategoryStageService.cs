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
    public class CategoryStageService : IGenericRepository<CategoryStages>
    {
        private readonly IMongoCollection<CategoryStages> _collection;

        public CategoryStageService(IMongoDatabase database)
        {
            _collection = database.GetCollection<CategoryStages>("CategoryStages");
        }

        public async Task AddAsync(CategoryStages entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<CategoryStages>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<CategoryStages> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(CategoryStages entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }
    }
}
