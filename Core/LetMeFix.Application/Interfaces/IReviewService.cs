using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Interfaces
{
    public interface IReviewService : IBaseService<Review>
    {
        Task<PagedResult<Review>> GetReviewsByJobId(PagedRequest request, string id);
        Task<PagedResult<Review>> GetReviewsByUserId(PagedRequest request, string id);
    }
}
