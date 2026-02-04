using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Interfaces
{
    public interface IOfferService
    {
        Task<PagedResult<Offers>> GetOffersByJobIdAsync(PagedRequest request, string jobId);
        Task<PagedResult<Offers>> GetOffersByCustomerIPerJobId(PagedRequest request, string jobId, string customerId);
        Task<bool> CreateOfferAsync(string jobId, string customerId, decimal price);
    }
}
