using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Abstraction
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(AppUser user);
    }
}
