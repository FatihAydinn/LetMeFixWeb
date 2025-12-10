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
    public class BaseService<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly IMongoCollection<T> _collection;
        public BaseService(IMongoDatabase database, string collName)
        {
            _collection = database.GetCollection<T>(collName);
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<T>> SearchFilter(string search, string filedName)
        {
            var filter = Builders<T>.Filter.Regex(filedName, new MongoDB.Bson.BsonRegularExpression(search, "i"));
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task<PagedResult<T>> PaginationAsync(int page = 1, int pageSize = 10)
        {
            var total = await _collection.CountDocumentsAsync(FilterDefinition<T>.Empty);
            var items = await _collection.Find(FilterDefinition<T>.Empty)
                                                .Skip((page - 1) * pageSize)
                                                .Limit(pageSize)
                                                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = (int)total,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
