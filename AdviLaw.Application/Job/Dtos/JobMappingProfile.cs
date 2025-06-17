//using AutoMapper;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AdviLaw.Application.Job.Dtos
//{
//    public class JobMappingProfile : Profile
//    {
//        public JobMappingProfile()
//        {
//            CreateMap<CreateJobDto, Domain.Entites.JobSection.Job>();  

//            CreateMap<Domain.Entites.JobSection.Job, JobDto>()
//                .ForMember(dest => dest.JobFieldName, opt => opt.MapFrom(src => src.JobField.Name))
//                .ForMember(dest => dest.LawyerFullName, opt => opt.MapFrom(src => src.Lawyer != null ? src.Lawyer.User.UserName : null));
//        }
//    }
//}
