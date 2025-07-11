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

            //GetJobByIdClientHandler
            CreateMap<Job, JobDetailsForClientDTO>()
                .ForMember(dest => dest.LawyerName, opt => opt.MapFrom(src => src.Lawyer != null && src.Lawyer.User != null ? src.Lawyer.User.UserName : ""))
                .ForMember(dest => dest.LawyerProfilePictureUrl, opt => opt.MapFrom(src => src.Lawyer != null && src.Lawyer.User != null ? src.Lawyer.User.ImageUrl : ""))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.User.UserName))
                .ForMember(dest => dest.ClientProfilePictureUrl, opt => opt.MapFrom(src => src.Client.User.ImageUrl))
                .ForMember(dest => dest.JobFieldName, opt => opt.MapFrom(src => src.JobField.Name))
                .ForMember(dest => dest.StatusLabel, opt => opt.MapFrom(src =>
                    src.Status == JobStatus.NotAssigned || src.Status == JobStatus.WaitingAppointment || src.Status == JobStatus.WaitingPayment ? "Pending" :
                    src.Status == JobStatus.Accepted ? "Accepted" :
                    src.Status == JobStatus.Rejected ? "Rejected" :
                    src.Status.ToString()
                ))
                .ReverseMap();

            //GetJobByIdLawyerHandler
            CreateMap<Job, JobDetailsForLawyerDTO>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.User.UserName))
                .ForMember(dest => dest.ClientProfilePictureUrl, opt => opt.MapFrom(src => src.Client.User.ImageUrl))
                .ForMember(dest => dest.JobFieldName, opt => opt.MapFrom(src => src.JobField.Name))
                .ForMember(dest => dest.LawyerName, opt => opt.MapFrom(src => src.Lawyer.User.UserName))
                .ReverseMap();


         
            CreateMap<Job, ClientConsultationDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Header, opt => opt.MapFrom(src => src.Header))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget))
                .ForMember(dest => dest.StatusLabel, opt => opt.MapFrom(src =>
                    src.Status == JobStatus.WaitingPayment ? "Waiting for Payment" :
                    src.Status == JobStatus.Started ? "In Progress" :
                    src.Status == JobStatus.Accepted ? "Accepted" :
                    src.Status == JobStatus.Rejected ? "Rejected" :
                    src.Status == JobStatus.NotAssigned || src.Status == JobStatus.WaitingAppointment ? "Pending" :
                    src.Status.ToString()
                ))
                .ForMember(dest => dest.LawyerName, opt => opt.MapFrom(src => src.Lawyer != null && src.Lawyer.User != null ? src.Lawyer.User.UserName : ""))
                .ForMember(dest => dest.LawyerProfilePictureUrl, opt => opt.MapFrom(src => src.Lawyer != null && src.Lawyer.User != null ? src.Lawyer.User.ImageUrl : ""))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.DurationHours))
                .ForMember(dest => dest.AppointmentTime, opt => opt.MapFrom(src => src.AppointmentTime))
                .ReverseMap();


            CreateMap<Job, JobListDTO>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
    .ForMember(dest => dest.Header, opt => opt.MapFrom(src => src.Header))
    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
    .ForMember(dest => dest.budget, opt => opt.MapFrom(src => src.Budget))
    .ForMember(dest => dest.IsAnonymus, opt => opt.MapFrom(src => src.IsAnonymus))
    .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
    .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null && src.Client.User != null ? src.Client.User.UserName : ""))
    .ForMember(dest => dest.ClientImageUrl, opt => opt.MapFrom(src => src.Client != null && src.Client.User != null ? src.Client.User.ImageUrl : ""))
    .ForMember(dest => dest.LawyerName, opt => opt.MapFrom(src => src.Lawyer != null && src.Lawyer.User != null ? src.Lawyer.User.UserName : ""))
    .ForMember(dest => dest.JobFieldId, opt => opt.MapFrom(src => src.JobFieldId))
    .ForMember(dest => dest.JobFieldName, opt => opt.MapFrom(src => src.JobField != null ? src.JobField.Name : ""))
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type))
    .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.DurationHours))
    .ForMember(dest => dest.AppointmentTime, opt => opt.MapFrom(src => src.AppointmentTime))
    .ReverseMap();

        }
    }
}
