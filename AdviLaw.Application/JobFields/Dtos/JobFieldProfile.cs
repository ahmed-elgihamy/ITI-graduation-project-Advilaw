using AdviLaw.Application.JobFields.Command.CreateJobField;
using AdviLaw.Application.JobFields.Command.DeleteJobField;
using AdviLaw.Application.JobFields.Command.UpdateJobField;
using AdviLaw.Domain.Entites.JobSection;
using AutoMapper;

namespace AdviLaw.Application.JobFields.Dtos
{
    public class JobFieldProfile : Profile
    {
        public JobFieldProfile()
        {
            CreateMap<CreateJobFieldCommand, JobField>();
            CreateMap<UpdateJobFieldCommand, JobField>();
            CreateMap<DeleteJobFieldCommand,JobField>();
            CreateMap<JobField, JobFieldDto>();
        }
    }
}
