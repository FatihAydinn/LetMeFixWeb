using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Persistence.Repository
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

        public async Task UpdateAsync(T entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<PagedResult<T>> GetAllAsync(PagedRequest request)
        {
            var filter = Builders<T>.Filter.Where(x => true);
            return await GetPagedWithFilterAsync(request, filter);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return filter;
        }

        public async Task<PagedResult<T>> GetPagedWithFilterAsync(PagedRequest request, FilterDefinition<T> filter)
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

        public async Task<PagedResult<T>> SearchFilter(PagedRequest request, string search, List<string> fieldNames)
        {
            var filter = new List<FilterDefinition<T>>();

            foreach (var field in fieldNames)
            {
                filter.Add(Builders<T>.Filter.Regex(field,
                    new BsonRegularExpression(search, "i")));
            }

            var finalFilter = Builders<T>.Filter.Or(filter);

            return await GetPagedWithFilterAsync(request, finalFilter);
        }

        public async Task<PagedResult<T>> FindAsync(PagedRequest request, FilterDefinition<T> filter)
        {
            return await GetPagedWithFilterAsync(request, filter);
        }

        public async Task UpdateWithFilter(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            await _collection.UpdateOneAsync(filter, update);
        }
    }
}
