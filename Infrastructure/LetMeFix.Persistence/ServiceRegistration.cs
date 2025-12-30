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
using LetMeFix.Application.Interfaces;
using LetMeFix.Persistence.Repository;

namespace LetMeFix.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            //services.AddSingleton<IGenericRepository<Job>, JobService>();
            //services.AddSingleton<IGenericRepository<Category>, CategoryService>();
            //services.AddSingleton<IGenericRepository<UserInformations>, UserInformationService>();
            services.AddSingleton<IGenericRepository<Languages>, LanguageService>();
            services.AddScoped<SkillsService>();
            //services.AddScoped<ContractService>();
            services.AddScoped<IGenericRepository<Contracts>, ContractRepository>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IGenericRepository<Offers>, OfferRepository>();
            services.AddScoped<IOfferService, OfferService>();
            //services.AddScoped<JobService>();
            services.AddScoped<UserInformationService>();
            services.AddScoped<ReviewService>();
            //services.AddScoped<OfferService>();
            //services.AddScoped<CategoryService>();
            services.AddScoped<IGenericRepository<Category>, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IGenericRepository<Job>, JobRepository>();
            services.AddScoped<IJobService, JobService>();
            //services.AddScoped<ChatSessionService>();
            services.AddScoped<IGenericRepository<ChatSession>, ChatSessionRepository>();
            services.AddScoped<IChatSessionService, ChatSessionService>();
            services.AddScoped<TranslationService>();
            //services.AddScoped<ReportService>();
            services.AddScoped<IGenericRepository<Reports>, ReportRepository>();
            services.AddScoped<IReportService, ReportService>();
            //services.AddSingleton<IGenericRepository<Offers>, OfferService>();
            //services.AddSingleton<IGenericRepository<CategoryStages>, CategoryStageService>();
        }
    }
}
