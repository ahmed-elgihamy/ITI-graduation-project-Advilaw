using AdviLaw.Application.Features.ProposalSection.Command;
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
            CreateMap<CreateProposalCommand, Proposal>()
                .ForMember(dest => dest.LawyerId, opt => opt.MapFrom(src => src.LawyerId))
                .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.JobId));
            CreateMap<Proposal, CreatedProposalDTO>().ReverseMap();
            CreateMap<Proposal, ProposalDetails>()
                .ForMember(dest => dest.LawyerName, opt => opt.MapFrom(src => src.Lawyer.User.UserName))
                .ForMember(dest => dest.JobHeader, opt => opt.MapFrom(src => src.Job.Header))
                .ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => src.Job.Description))
                .ForMember(dest => dest.JobBudget, opt => opt.MapFrom(src => src.Job.Budget));
        }
    }
}
