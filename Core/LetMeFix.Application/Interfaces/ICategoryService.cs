using LetMeFix.Application.DTOs;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Dictionary<string, string>> GetPreviousCategory(string id);
        Task<PagedResult<Category>> SearchCategory(string value, PagedRequest request);
    }
}
