using AdviLaw.Application.Features.Lawyers.Queries.DTOs;
using AdviLaw.Domain.Entities.UserSection;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Lawyers.Queries.GetAllLawyers
{
    public class LawyerMappingProfile : Profile
    {
        public LawyerMappingProfile()
        {
            CreateMap<Lawyer, LawyerListItemDto>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.UserName))
                    .ForMember(dest => dest.JobFieldNames, opt =>
                        opt.MapFrom(src => src.Fields.Select(f => f.JobField.Name).ToList()))
                    
   

                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.User.Country ?? "not found"))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User.City ?? "not found"))
                .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience))
                //.ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.User.Rating)) 
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.User.ImageUrl) ? "default.jpg" : src.User.ImageUrl))
                .ForMember(dest => dest.CaseCount, opt => opt.MapFrom(src => src.Jobs.Count));
        }

    }
}
