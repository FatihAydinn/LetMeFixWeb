using LetMeFix.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Interfaces
{
    public interface IUserInformationService
    {
        Task UpdateAddress(UserinformationAddressDTO entity);
        Task UpdateSocials(UserInformationSocialsDTO entity);
        Task UpdateSummary(UserInformationSummaryDTO entity);
    }
}
