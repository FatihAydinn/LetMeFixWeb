using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Interfaces
{
    public interface ISkillsService : IBaseService<Skills>
    {
        Task<PagedResult<Skills>> SearchFilter(PagedRequest request, string search, List<string> fieldNames);
    }
}
