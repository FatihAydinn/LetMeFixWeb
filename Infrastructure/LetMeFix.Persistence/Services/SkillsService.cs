using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
            await base.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<List<Skills>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Skills> GetByIdAsync(string id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Skills entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<Skills>> GetSkillsbyCategory(string category)
        {
            return await _collection.Find(x => x.RelatedCategories.Contains(category)).ToListAsync();
        }

        public async Task<PagedResult<Skills>> GetPaged(PagedRequest request, FilterDefinition<Skills> filter)
        {
            return await GetPagedWithFilterAsync(request, filter);
        }

        public async Task<PagedResult<Skills>> SearchSkill(PagedRequest request, string value)
        {
            var fields = new List<string> { "SkillTitle" };
            return await SearchFilter(request, value, fields);
        }
    }
}
