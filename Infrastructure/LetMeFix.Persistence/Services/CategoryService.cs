using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Persistence.Services
{
    public class CategoryService : BaseService<Category>
    {
        public CategoryService(IMongoDatabase database) : base (database, "CategoryStages")
        {
        }

        public async Task AddAsync(Category entity)
        {
            await base.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Category entity)
        {
            await base.UpdateAsync(entity);
        }

        //public async Task<string> GetNamebyId(string id)
        //{
        //    var val = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        //    return val.Name.ToString();
        //}

        public async Task<Dictionary<string, string>> GetPreviousCategory(string id)
        {
            Dictionary<string, string> fullPath = new();            
            var prev = await base.GetByIdAsync(id);
            if (prev != null) fullPath = prev.FullPaths;
            //else fullPath = "";
            return fullPath;
        }

        public async Task<PagedResult<Category>> GetCategoriesPages(FilterDefinition<Category> filter, PagedRequest request)
        {
            return await GetPagedWithFilterAsync(filter, request);
        }

        public async Task<PagedResult<Category>> SearchCategory(string value, PagedRequest request)
        {
            var fields = new List<string> { "FullPath", "Name" };
            return await SearchFilter(value, fields, request);
        }
    }
}
