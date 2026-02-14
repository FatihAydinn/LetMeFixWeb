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
    public interface IBaseService<T> where T : class
    {
        Task<PagedResult<T>> GetAllAsync(PagedRequest request);
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
        Task<PagedResult<T>> GetPagedWithFilterAsync(PagedRequest request, FilterDefinition<T> filter);
    }
}
