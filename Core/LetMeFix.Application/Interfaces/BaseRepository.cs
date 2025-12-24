using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Interfaces
{
    public class BaseRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection;
        public BaseRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<PagedResult<T>> GetPagedWithFilterAsync(MongoDB.Driver.FilterDefinition<T> filter, PagedRequest request)
        {
            var total = await _collection.CountDocumentsAsync(filter);
            var items = await _collection.Find(filter)
                                         .Skip((request.Page - 1) * request.PageSize)
                                         .Limit(request.PageSize)
                                         .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = (int)total,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }

        public async Task<PagedResult<T>> SearchFilter(string search, List<string> fieldNames, PagedRequest request)
        {
            var filter = new List<FilterDefinition<T>>();

            foreach (var field in fieldNames)
            {
                filter.Add(Builders<T>.Filter.Regex(field,
                    new BsonRegularExpression(search, "i")));
            }

            var finalFilter = Builders<T>.Filter.Or(filter);

            return await GetPagedWithFilterAsync(finalFilter, request);
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
