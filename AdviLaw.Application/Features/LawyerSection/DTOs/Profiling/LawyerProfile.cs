using AdviLaw.Domain.Entities.UserSection;
using AutoMapper;

namespace AdviLaw.Application.Features.LawyerSection.DTOs.Profiling
{
    public class LawyerProfile : Profile
    {
        public LawyerProfile()
        {
            CreateMap<Lawyer, LawyerListDTO>().ReverseMap();
        }
    }
}
