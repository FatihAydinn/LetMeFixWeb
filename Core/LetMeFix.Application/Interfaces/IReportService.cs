using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Interfaces
{
    public interface IReportService
    {
        Task AddResultToReport(string id, string reason);
        Task<PagedResult<Reports>> GetReportsByUser(PagedRequest request, string userId);
    }
}
