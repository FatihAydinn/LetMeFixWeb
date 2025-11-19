using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace LetMeFix.Persistence.Services
{
    public class SkillsService : BaseService<Skills>
    {
        //private readonly IMongoCollection<Skills> _collection;
        public SkillsService(IMongoDatabase database) : base (database, "Skills")
        { }

        public async Task AddAsync(Skills entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<Skills>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<Skills> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Skills entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task<List<Skills>> GetSkillsbyCategory(string category)
        {
            return await _collection.Find(x => x.RelatedCategories.Contains(category)).ToListAsync();
        }
    }
}
