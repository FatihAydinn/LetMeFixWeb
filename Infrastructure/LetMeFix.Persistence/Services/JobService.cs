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
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.Infrastructure.Services
{
    public class JobService : BaseService<Job>
    {
        CategoryService _categoryStages;
        public JobService(IMongoDatabase database, CategoryService categoryStages) : base (database, "Jobs") 
        {
            _categoryStages = categoryStages;
        }

        private async Task GetCategoryPaths(Job item)
        {
            if (item != null)
            {
                var categoryfullpath = await _categoryStages.GetByIdAsync(item.CategoryId.Substring(item.CategoryId.Length - 3));
                item.CategoryPath = categoryfullpath.FullPaths["EN"];
            }
        }

        public async Task AddAsync(Job job)
        {
            await base.AddAsync(job);
        }

        public async Task DeleteAsync(string id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<List<Job>> GetAllAsync()
        {
            var values = await base.GetAllAsync();
            foreach (var item in values)
            {
                await GetCategoryPaths(item);
                //var categoryfullpath = await _categoryStages.GetByIdAsync(item.CategoryId.Substring(item.CategoryId.Length - 3));
                //item.CategoryId = categoryfullpath.FullPaths["EN"];
            }
            return values; 
        }

        public async Task<Job> GetByIdAsync(string id)
        {
            var value = await base.GetByIdAsync(id);
            await GetCategoryPaths(value);

            return value;
        }

        public async Task UpdateAsync(Job job)
        {
            await base.UpdateAsync(job);
        }

        public async Task<List<Job>> ListJobsPerUser(string userId)
        {
            return await _collection.Find(x => x.ProviderId == userId).ToListAsync();
        } 

        public async Task<PagedResult<Job>> ListJobsPerCategory(string categoryId, PagedRequest request)
        {
            var filter = Builders<Job>.Filter.Where(x => x.CategoryId.Length % 3 == 0 && x.CategoryId.Contains(categoryId));
            //var value = await _collection.Find(x => x.CategoryId.Length % 3 == 0 && x.CategoryId.Contains(categoryId)).ToListAsync();
            return await GetJobsPaged(filter, request);
        }

        public async Task<List<Job>> SearchJob(string search, string filedName = "Title")
        {
            return await base.SearchFilter(search, filedName);
        }

        public async Task<PagedResult<Job>> GetJobsPaged(FilterDefinition<Job> filter, PagedRequest request)
        {
            return await GetPagedWithFilterAsync(filter, request);
        }
    }
}
