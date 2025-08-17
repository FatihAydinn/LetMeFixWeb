using LetMeFix.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Persistence
{
    public class UserDbContext : IdentityDbContext<AppUser>
    {
        public UserDbContext(DbContextOptions option): base(option)
        {
            
        }
    }
}
