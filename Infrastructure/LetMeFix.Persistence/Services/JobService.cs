using LetMeFix.Infrastructure.Services;
using LetMeFix.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetMeFix.Domain.Interfaces;
using LetMeFix.Persistence.Services;
using static MongoDB.Driver.WriteConcern;
using SharpCompress.Common;

namespace LetMeFix.Infrastructure.Services
{
    public class JobService : BaseService<Job>
    {
        CategoryService _categoryStages;
        public JobService(IMongoDatabase database, CategoryService categoryStages) : base (database, "Jobs") 
        {
            _categoryStages = categoryStages;
        }

        public async Task AddAsync(Job job)
        {
            await _collection.InsertOneAsync(job);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(t => t.Id == id);
        }

        public async Task<List<Job>> GetAllAsync()
        {
            var values = await _collection.Find(x => true).ToListAsync();
            foreach (var item in values)
            {
                var categoryfullpath = await _categoryStages.GetByIdAsync(item.CategoryId.Substring(item.CategoryId.Length - 3));
                item.CategoryId = categoryfullpath.FullPaths["EN"];
            }
            return values; 
        }

        public async Task<Job> GetByIdAsync(string id)
        {
            var value = await _collection.Find(t => t.Id == id).FirstOrDefaultAsync();
            var categoryfullpath = await _categoryStages.GetByIdAsync(value.CategoryId.Substring(value.CategoryId.Length - 3));
            value.CategoryId = categoryfullpath.FullPaths["EN"];
            return value;
        }

        public async Task UpdateAsync(Job job)
        {
            await _collection.ReplaceOneAsync(t => t.Id == job.Id, job);
        }
    }
}
