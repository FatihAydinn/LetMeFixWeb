using AutoMapper;
using Humanizer;
using LetMeFix.Application.DTOs;
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
    public class CategoryService : BaseService<Category, CategoryDTO>, ICategoryService
    {
        private readonly IGenericRepository<Languages> _language;

        public CategoryService(IGenericRepository<Category> repository, IGenericRepository<Languages> language, IMapper mapper) : base(repository, mapper) 
        {
            _language = language;
        }

        public async Task CreateCategoryStage(CategoryDTO dto)
        {
            var entity = _mapper.Map<Category>(dto);
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

        public async Task<PagedResult<CategoryDTO>> GetCategoriesPages(FilterDefinition<Category> filter, PagedRequest request)
        {
            var result = await _repository.GetPagedWithFilterAsync(request, filter);
            return new PagedResult<CategoryDTO>
            {
                Items = _mapper.Map<List<CategoryDTO>>(result.Items),
                TotalCount = result.TotalCount,
                Page = result.Page,
                PageSize = result.PageSize
            };
        }

        public async Task<PagedResult<CategoryDTO>> SearchCategory(string value, PagedRequest request)
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

            var entityResult = await _repository.SearchFilter(request, value, fields);

            return new PagedResult<CategoryDTO>
            {
                Items = _mapper.Map<List<CategoryDTO>>(entityResult.Items),
                TotalCount = entityResult.TotalCount,
                Page = entityResult.Page,
                PageSize = entityResult.PageSize
            };
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
