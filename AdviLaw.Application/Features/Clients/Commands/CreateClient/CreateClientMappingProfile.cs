using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.DTOs.Client;
using AdviLaw.Application.DTOs.Lawyer;
using AdviLaw.Application.DTOs.Users;
using AdviLaw.Application.Features.Lawyers.Commands.CreateLawyer;
using AdviLaw.Domain.Entities.UserSection;
using AutoMapper;

namespace AdviLaw.Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientMappingProfile: Profile
    {
        public CreateClientMappingProfile()
        {
            CreateMap<CreateClientCommand, Client>()
            .ReverseMap();

            CreateMap<Client, ClientResponseDto>();
            CreateMap<UserRegisterDto, CreateClientCommand>();
        }

    }


}
