using LetMeFix.Application.Abstraction;
using LetMeFix.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Persistence.Concretes
{
    public class CategoryService : IGenericRepository<Category>
    {
        private readonly IMongoCollection<Category> _category;

        public CategoryService(IMongoDatabase category)
        {
            _category = category.GetCollection<Category>("Category");
        }

        public async Task AddAsync(Category entity)
        {
            await _category.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _category.DeleteOneAsync(id);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _category.Find(x => true).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            return await _category.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Category entity)
        {
            await _category.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }
    }
}
