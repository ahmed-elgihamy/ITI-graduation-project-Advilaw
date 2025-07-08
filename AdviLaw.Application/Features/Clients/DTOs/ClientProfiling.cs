using AdviLaw.Domain.Entities.UserSection;
using AutoMapper;

namespace AdviLaw.Application.Features.Clients.DTOs
{
    public class ClientProfiling : Profile
    {
        public ClientProfiling() {

            CreateMap<Client, ClientProfileDTO>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User!.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.User!.Country))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.User!.CountryCode))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.User!.PostalCode))
                .ForMember(dest => dest.NationalityId, opt => opt.MapFrom(src => src.User!.NationalityId))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.User!.ImageUrl))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.User!.IsActive))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User!.Gender))
                .ForMember(dest => dest.StripeAccountId, opt => opt.MapFrom(src => src.User!.StripeAccountId))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.User!.Balance))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.User!.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.User!.UpdatedAt))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.LastLoginAt, opt => opt.MapFrom(src => src.User!.LastLoginAt));

        }
    }
}
