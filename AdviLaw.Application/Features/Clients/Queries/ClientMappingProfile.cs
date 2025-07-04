using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.DTOs.Client;
using AdviLaw.Domain.Entities.UserSection;
using AutoMapper;

namespace AdviLaw.Application.Features.Clients.Queries
{
    public class ClientMappingProfile: Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<Client, ClientListDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.NationalityId, opt => opt.MapFrom(src => src.User.NationalityId));


        }

    }
}
