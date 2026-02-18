using LetMeFix.Application.DTOs;
using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Interfaces
{
    public interface IContractService : IBaseService<Contracts, ContractsDTO>
    {
        Task<PagedResult<Contracts>> GetContractsByProviderId(PagedRequest request, string userId);
        Task<PagedResult<Contracts>> GetContractsByClientId(PagedRequest request, string userId);
        Task GiveATip(string id, decimal tip);
        Task ChangeStatus(string id, int status);
        Task<PagedResult<Contracts>> GetContractByStatusAndUserId(PagedRequest request, string userId, int status);
    }
}
