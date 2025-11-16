using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetMeFix.Domain.Entities;
using LetMeFix.Application.DTOs;

namespace LetMeFix.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDTO, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Lastname));

            CreateMap<UserInformations, UserInformationSocialsDTO>().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UserInformations, UserinformationAddressDTO>().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UserInformations, UserInformationSummaryDTO>().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ChatSessionDTO, ChatSession>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.MessageContent, opt => opt.MapFrom(src => new List<MessageContent>()));
        }
    }
}
