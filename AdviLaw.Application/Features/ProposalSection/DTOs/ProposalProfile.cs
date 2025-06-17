using AdviLaw.Domain.Entites.ProposalSection;
using AutoMapper;

namespace AdviLaw.Application.Features.ProposalSection.DTOs
{
    public class ProposalProfile : Profile
    {
        public ProposalProfile()
        {
            CreateMap<Proposal, ProposalListDTO>()
                .ForMember(dest => dest.LawyerName, opt => opt.MapFrom(src => src.Lawyer.User.UserName));
        }
    }
}
