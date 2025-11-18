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
    public class SkillsService : IGenericRepository<Skills>
    {
        private readonly IMongoCollection<Skills> _skills;
        public SkillsService(IMongoDatabase skills)
        {
            _skills = skills.GetCollection<Skills>("Skills");
        }

        public async Task AddAsync(Skills entity)
        {
            await _skills.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _skills.DeleteOneAsync(x => x.SkillId == id);
        }

        public async Task<List<Skills>> GetAllAsync()
        {
            return await _skills.Find(x => true).ToListAsync();
        }

        public async Task<Skills> GetByIdAsync(string id)
        {
            return await _skills.Find(x => x.SkillId == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Skills entity)
        {
            await _skills.ReplaceOneAsync(x => x.SkillId == entity.SkillId, entity);
        }

        public async Task<Skills> GetSkillsbyCategory(string category)
        {
            return await _skills.Find(x => x.RelatedCategories.Contains(category)).FirstOrDefaultAsync();
        }
    }
}
