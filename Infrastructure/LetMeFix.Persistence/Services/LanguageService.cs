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
            await _languages.DeleteOneAsync(x => x.LanguageId == id);
        }

        public async Task<List<Languages>> GetAllAsync()
        {
            return await _languages.Find(x => true).ToListAsync();
        }

        public async Task<Languages> GetByIdAsync(string id)
        {
            return await _languages.Find(x => x.LanguageId == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Languages entity)
        {
            await _languages.ReplaceOneAsync(x => x.LanguageId == entity.LanguageId, entity);
        }
    }
}
