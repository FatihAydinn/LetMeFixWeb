using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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

        public async Task<PagedResult<T>> SearchFilter(string search, List<string> fieldNames, PagedRequest request)
        {
            //var filter = Builders<T>.Filter.Regex(fieldName, new MongoDB.Bson.BsonRegularExpression(search, "i"));
            var filter = new List<FilterDefinition<T>>();

            foreach (var field in fieldNames)
            {
                filter.Add(Builders<T>.Filter.Regex(field,
                    new BsonRegularExpression(search, "i")));
            }

            var finalFilter = Builders<T>.Filter.Or(filter);

            return await GetPagedWithFilterAsync(finalFilter, request);          
        }

        public async Task UpdateAsync(T entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task<PagedResult<T>> PaginationAsync(PagedRequest request)
        {
            var total = await _collection.CountDocumentsAsync(FilterDefinition<T>.Empty);
            var items = await _collection.Find(FilterDefinition<T>.Empty)
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

        public async Task<PagedResult<T>> GetPagedWithFilterAsync(FilterDefinition<T> filter, PagedRequest request)
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
    }
}
