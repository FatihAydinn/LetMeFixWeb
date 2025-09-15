using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(AppUser user);
        string GenerateRefreshToken();
        Task<bool> RevokeRefreshToken(string userId);
    }
}
