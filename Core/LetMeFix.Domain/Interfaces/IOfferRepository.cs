using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Interfaces
{
    public interface IOfferRepository : IGenericRepository<Offers>
    {
        Task<List<Offers>> GetOffersByJobIdAsync(string jobId);
        Task<List<Offers>> GetOffersByCustomerIPerJobId(string jobId, string customerId);
    }
}
