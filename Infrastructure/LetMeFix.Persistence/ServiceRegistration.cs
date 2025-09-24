using LetMeFix.Domain.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetMeFix.Persistence.Services;

namespace LetMeFix.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddSingleton<IGenericRepository<Ticket>, TicketService>();
            services.AddSingleton<IGenericRepository<Category>, CategoryService>();
            services.AddSingleton<IGenericRepository<UserInformations>, UserInformationService>();
        }
    }
}
