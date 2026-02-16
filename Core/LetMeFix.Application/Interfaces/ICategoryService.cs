using LetMeFix.Application.DTOs;
using LetMeFix.Application.Services;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Interfaces
{
    public interface ICategoryService : IBaseService<Category, CategoryDTO>
    {
        Task CreateCategoryStage(CategoryDTO entity);
        Task<Dictionary<string, string>> GetPreviousCategory(string id);
        Task<PagedResult<CategoryDTO>> SearchCategory(string value, PagedRequest request);
    }
}
