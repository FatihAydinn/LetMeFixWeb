using LetMeFix.Application.Interfaces;
using LetMeFix.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IChatSessionService, ChatSessionService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ISavedJobService, SavedJobService>();
            services.AddScoped<ISkillsService, SkillsService>();
            services.AddScoped<IUserInformationService, UserInformationService>();
            services.AddScoped<ITranslationService, TranslationService>();
            services.AddScoped<ILanguageService, LanguageService>();
        }
    }
}
