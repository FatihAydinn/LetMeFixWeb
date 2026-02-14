using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Services
{
    //!!
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected readonly IGenericRepository<T> _repository;
        public BaseService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<PagedResult<T>> GetAllAsync(PagedRequest request)
        {
            return await _repository.GetAllAsync(request);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task<PagedResult<T>> GetPagedWithFilterAsync(PagedRequest request, FilterDefinition<T> filter)
        {
            return await _repository.GetPagedWithFilterAsync(request, filter);
        }

        public virtual async Task<PagedResult<T>> SearchFilter(PagedRequest request, string search, List<string> fieldNames)
        {
            return await _repository.SearchFilter(request, search, fieldNames);
        }

        public virtual async Task<PagedResult<T>> FindAsync(PagedRequest request, FilterDefinition<T> filter)
        {
            return await _repository.FindAsync(request, filter);
        }

        public virtual async Task UpdateWithFilter(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            await _repository.UpdateWithFilter(filter, update);
        }
    }
}
