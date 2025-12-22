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

        public async Task<PagedResult<Skills>> GetPaged(FilterDefinition<Skills> filter, PagedRequest request)
        {
            return await GetPagedWithFilterAsync(filter, request);
        }

        public async Task<PagedResult<Skills>> SearchSkill(string value, PagedRequest request)
        {
            return await SearchFilter(value, "SkillTitle", request);
        }
    }
}
