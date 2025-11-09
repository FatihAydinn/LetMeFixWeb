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
    public class TranslationService : BaseService<Translations>
    {
        public TranslationService(IMongoDatabase database) : base (database, "Translations")
        {
        }

        public async Task AddAsync(Translations entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<Translations>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<Translations> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Translations entity)
        {
            await _collection.DeleteOneAsync(x => x.Id == entity.Id);
        }
    }
}
