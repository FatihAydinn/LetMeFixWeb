using LetMeFix.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Interfaces
{
    public interface IBaseService<TEntity, TDto> 
        where TEntity : class
        where TDto : class
    {
        Task<PagedResult<TDto>> GetAllAsync(PagedRequest request);
        Task<TDto> GetByIdAsync(string id);
        Task AddAsync(TDto entity);
        Task UpdateAsync(TDto entity);
        Task DeleteAsync(string id);
        Task<PagedResult<TDto>> GetPagedWithFilterAsync(PagedRequest request, FilterDefinition<TEntity> filter);
    }
}
