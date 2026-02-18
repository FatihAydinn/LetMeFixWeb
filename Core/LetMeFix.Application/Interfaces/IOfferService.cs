using LetMeFix.Application.DTOs;
using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Interfaces
{
    public interface IOfferService : IBaseService<Offers, OffersDTO>
    {
        Task<PagedResult<OffersDTO>> GetOffersByJobIdAsync(PagedRequest request, string jobId);
        Task<PagedResult<OffersDTO>> GetOffersByCustomerIPerJobId(PagedRequest request, string jobId, string customerId);
        Task<bool> CreateOfferAsync(OffersDTO offer);
    }
}
