using AdviLaw.Application.Features.JobSection.Commands.CreateJob;
using AdviLaw.Domain.Entites.JobSection;
using AutoMapper;

namespace AdviLaw.Application.Features.JobSection.DTOs.Profiling
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<Job, JobListForLawyerDTO>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.User.UserName))
                .ForMember(dest => dest.ClientImageUrl, opt => opt.MapFrom(src => src.Client.User.ImageUrl))
                .ForMember(dest => dest.JobFieldName, opt => opt.MapFrom(src => src.JobField.Name))
                .ReverseMap();

            CreateMap<Job, JobListForClientDTO>()
                .ForMember(dest => dest.LawyerName, opt => opt.MapFrom(src => src.Lawyer.User.UserName))
                .ForMember(dest => dest.LawyerImageUrl, opt => opt.MapFrom(src => src.Lawyer.User.ImageUrl))
                .ForMember(dest => dest.JobFieldName, opt => opt.MapFrom(src => src.JobField.Name))
                .ReverseMap();

            CreateMap<Job, CreatedJobDTO>();
            CreateMap<CreateJobCommand, Job>();
            CreateMap<CreateJobDTO, CreateJobCommand>();
        }
    }
}
