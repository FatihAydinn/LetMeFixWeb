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
            await base.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<List<Translations>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Translations> GetByIdAsync(string id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Translations entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<Translations>> SearchFilter(string search, string filedName)
        {
            return await base.SearchFilter(search, filedName);
        }

        public async Task<PagedResult<Translations>> GetByPage(string langId, int page, int pageSize)
        {
            var filter = Builders<Translations>.Filter.Eq(x => x.LanguageId, langId);
            return await GetPagedWithFilterAsync(filter, page, pageSize);
        }
    }
}
