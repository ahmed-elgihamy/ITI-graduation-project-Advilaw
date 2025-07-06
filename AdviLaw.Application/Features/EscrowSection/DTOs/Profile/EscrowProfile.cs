using AdviLaw.Application.Features.EscrowSection.DTOs;
using AdviLaw.Domain.Entites.EscrowTransactionSection;
using AutoMapper;

public class EscrowProfile : Profile
{
    public EscrowProfile()
    {
        CreateMap<EscrowTransaction, CreatedEscrowDTO>()
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.ToString().ToLower()));
    }
}