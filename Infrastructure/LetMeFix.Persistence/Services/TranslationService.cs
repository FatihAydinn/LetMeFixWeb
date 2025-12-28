using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<PagedResult<Translations>> GetAllAsync(PagedRequest request)
        {
            return await base.GetAllAsync(request);
        }

        public async Task<Translations> GetByIdAsync(string id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Translations entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<PagedResult<Translations>> SearchTranslation(PagedRequest request, string search)
        {
            var fields = new List<string> { "Key", "Content" };
            return await SearchFilter(request ,search, fields);
        }

        public async Task<PagedResult<Translations>> GetByPage(PagedRequest request, string langId)
        {
            var filter = Builders<Translations>.Filter.Eq(x => x.LanguageId, langId);
            return await GetPagedWithFilterAsync(request, filter);
        }
    }
}
