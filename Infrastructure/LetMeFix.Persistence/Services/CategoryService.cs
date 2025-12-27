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

namespace LetMeFix.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repository;

        public CategoryService(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<Category>> GetCategoriesPages(FilterDefinition<Category> filter, PagedRequest request)
        {
            return await _repository.GetPagedWithFilterAsync(filter, request);
        }

        public async Task<PagedResult<Category>> SearchCategory(string value, PagedRequest request)
        {
            var fields = new List<string> { "Id" };
            return await _repository.SearchFilter(value, fields, request);
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
