using LetMeFix.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<PagedResult<T>> GetAllAsync(PagedRequest request);
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
        Task<PagedResult<T>> SearchFilter(PagedRequest request, string search, List<string> fieldNames);
        Task<PagedResult<T>> GetPagedWithFilterAsync(PagedRequest request, FilterDefinition<T> filter);
        Task<PagedResult<T>> FindAsync(PagedRequest request, FilterDefinition<T> filter);
        Task UpdateWithFilter(FilterDefinition<T> filter, UpdateDefinition<T> update);
    }
}
