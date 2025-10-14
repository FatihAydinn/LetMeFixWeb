using LetMeFix.Domain.Interfaces;
using LetMeFix.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace LetMeFix.Persistence.Services
{
    public class CategoryService : BaseService<Category>
    {
        CategoryStageService _stage;
        public CategoryService(IMongoDatabase category, CategoryStageService stage) : base (category, "Category") 
        {
            _stage = stage;
        }

        public async Task AddAsync(Category entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            var fullpath = await GetCategoryNames(id);
            var val = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            val.FullPath = fullpath;
            return val;
        }

        public async Task UpdateAsync(Category entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task<string> GetCategoryNames(string id)
        {
            var idsegments = new List<string>();
            for (int i = 0; i < id.Length; i += 3)
            {
                idsegments.Add(id.Substring(i,3));
            }
            var idsplit = Enumerable.Range(0, 3).Select(x => id.Substring(x * 3, 3));
            string fullpath = "";
            foreach (var name in idsegments) {
                fullpath += await _stage.GetNamebyId(name) + "/";
            }
            return fullpath.Substring(0, fullpath.Length - 1);
            //return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
