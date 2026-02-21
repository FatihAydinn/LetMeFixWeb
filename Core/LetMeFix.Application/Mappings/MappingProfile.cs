using AutoMapper;
using LetMeFix.Application.DTOs;
using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LetMeFix.Application.DTOs.CategoryDTO;

namespace LetMeFix.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDTO, AppUser>();

            CreateMap<UserInformations, UserInformationSocialsDTO>().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UserInformations, UserinformationAddressDTO>().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UserInformations, UserInformationSummaryDTO>().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ChatSession, ChatSessionDTO>().ReverseMap();
            CreateMap<MessageContent, MessageContentDTO>().ReverseMap();
            CreateMap<PreviousMessages, PreviousMessagesDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Contracts, ContractsDTO>().ReverseMap();
            CreateMap<Job, JobDTO>().ReverseMap();
            CreateMap<Languages, LanguagesDTO>().ReverseMap();
            CreateMap<Offers, OffersDTO>().ReverseMap();
            CreateMap<Reports, ReportsDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<SavedJobs, SavedJobsDTO>().ReverseMap();
            CreateMap<Skills, SkillsDTO>().ReverseMap();
            CreateMap<Translations, TranslationsDTO>().ReverseMap();
            CreateMap<UserInformations, UserInformationsDTO>().ReverseMap();
        }
    }
}
