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
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
        Task<PagedResult<T>> SearchFilter(string search, List<string> fieldNames, PagedRequest request);
        Task<PagedResult<T>> GetPagedWithFilterAsync(FilterDefinition<T> filter, PagedRequest request);
        Task<List<T>> FindAsync(FilterDefinition<T> filter);
    }
}
