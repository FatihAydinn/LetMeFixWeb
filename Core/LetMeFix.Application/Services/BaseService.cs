using AutoMapper;
using Azure.Core;
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
    public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto> 
        where TEntity : class
        where TDto : class
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public BaseService(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<PagedResult<TDto>> GetAllAsync(PagedRequest request)
        {
            var pagedEntities = await _repository.GetAllAsync(request);
            return new PagedResult<TDto>
            {
                Items = _mapper.Map<List<TDto>>(pagedEntities.Items),
                TotalCount = pagedEntities.TotalCount,
                Page = pagedEntities.Page,
                PageSize = pagedEntities.PageSize
            };
        }

        public async Task<TDto> GetByIdAsync(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public async Task UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.UpdateAsync(entity);
        }

        public async Task<PagedResult<TDto>> GetPagedWithFilterAsync(PagedRequest request, FilterDefinition<TEntity> filter)
        {
            var pagedEntities = await _repository.GetPagedWithFilterAsync(request, filter);
            return new PagedResult<TDto>
            {
                Items = _mapper.Map<List<TDto>>(pagedEntities.Items),
                TotalCount = pagedEntities.TotalCount,
                Page = pagedEntities.Page,
                PageSize = pagedEntities.PageSize
            };
        }

        public virtual async Task<PagedResult<TDto>> SearchFilter(PagedRequest request, string search, List<string> fieldNames)
        {
            var pagedEntities = await _repository.SearchFilter(request, search, fieldNames);
            return new PagedResult<TDto>
            {
                Items = _mapper.Map<List<TDto>>(pagedEntities.Items),
                TotalCount = pagedEntities.TotalCount,
                Page = pagedEntities.Page,
                PageSize = pagedEntities.PageSize
            };
        }

        public virtual async Task<PagedResult<TDto>> FindAsync(PagedRequest request, FilterDefinition<TEntity> filter)
        {
            var pagedEntities = await _repository.FindAsync(request, filter);
            return new PagedResult<TDto>
            {
                Items = _mapper.Map<List<TDto>>(pagedEntities.Items),
                TotalCount = pagedEntities.TotalCount,
                Page = pagedEntities.Page,
                PageSize = pagedEntities.PageSize
            };
        }

        public virtual async Task UpdateWithFilter(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            await _repository.UpdateWithFilter(filter, update);
        }
    }
}
