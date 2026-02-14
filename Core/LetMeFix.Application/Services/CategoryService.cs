using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly IGenericRepository<Languages> _language;

        public CategoryService(IGenericRepository<Category> repository, IGenericRepository<Languages> language) : base(repository) 
        {
            _language = language;
        }

        public async Task CreateCategoryStage(Category entity)
        {
            if (entity.PreviousParent == null) entity.FullPaths = entity.Names;
            else
            {
                var prev = await GetPreviousCategory(entity.PreviousParent);
                entity.FullPaths = entity.Names.ToDictionary(x => x.Key, x =>
                {
                    string previousCategoryName = prev.ContainsKey(x.Key) ? prev[x.Key] : "";
                    return $"{previousCategoryName} > {x.Value}";
                });
            }
            await _repository.AddAsync(entity);
        }

        public async Task<PagedResult<Category>> GetCategoriesPages(FilterDefinition<Category> filter, PagedRequest request)
        {
            return await _repository.GetPagedWithFilterAsync(request, filter);
        }

        public async Task<PagedResult<Category>> SearchCategory(string value, PagedRequest request)
        {
            PagedRequest request2 = new PagedRequest();
            request2.Page = 1;
            request2.PageSize = 100;
            var langs = await _language.GetAllAsync(request2);
            var fields = new List<string> { "Id" };
            foreach (var lang in langs.Items)
            {
                fields.Add($"Names.{lang.LanguageCode}");
            }
            return await _repository.SearchFilter(request, value, fields);
        }

        public async Task<Dictionary<string, string>> GetPreviousCategory(string id)
        {
            Dictionary<string, string> fullPath = new();
            var prev = await _repository.GetByIdAsync(id);
            if (prev != null) fullPath = prev.FullPaths;
            return fullPath;
        }

    }
}
