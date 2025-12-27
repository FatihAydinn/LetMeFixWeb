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
    public class LanguageService : IGenericRepository<Languages>
    {
        private readonly IMongoCollection<Languages> _languages;
        public LanguageService(IMongoDatabase languages)
        {
            _languages = languages.GetCollection<Languages>("Languages");
        }

        public async Task AddAsync(Languages entity)
        {
            await _languages.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _languages.DeleteOneAsync(x => x.Id == id);
        }

        public Task<List<Languages>> FindAsync(FilterDefinition<Languages> filter)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Languages>> FindAsync(PagedRequest request, FilterDefinition<Languages> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Languages>> GetAllAsync()
        {
            return await _languages.Find(x => true).ToListAsync();
        }

        public async Task<Languages> GetByIdAsync(string id)
        {
            return await _languages.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<PagedResult<Languages>> GetPagedWithFilterAsync(FilterDefinition<Languages> filter, PagedRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Languages>> GetPagedWithFilterAsync(PagedRequest request, FilterDefinition<Languages> filter)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Languages>> SearchFilter(string search, List<string> fieldNames, PagedRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Languages>> SearchFilter(PagedRequest request, string search, List<string> fieldNames)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Languages entity)
        {
            await _languages.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public Task UpdateWithFilter(FilterDefinition<Languages> filter, UpdateDefinition<Languages> update)
        {
            throw new NotImplementedException();
        }
    }
}
