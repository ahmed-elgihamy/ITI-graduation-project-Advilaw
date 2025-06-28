using AdviLaw.Domain.Entites.PaymentSection;
using AutoMapper;

namespace AdviLaw.Application.Features.PaymentSection.DTOs.Profiling
{
    public class PaymentProfiling : Profile
    {
        public PaymentProfiling()
        {
            CreateMap<Payment, LawyerPaymentListDTO>()
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.UserName))
                .ForMember(dest => dest.SenderImgUrl, opt => opt.MapFrom(src => src.Sender.ImageUrl))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.EscrowTransaction.Amount))
                .ReverseMap();
        }
    }
}
