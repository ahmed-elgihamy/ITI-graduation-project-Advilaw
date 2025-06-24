using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Application.Features.JobSection.Queries.GetPagedJobs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Shared.DTOs
{
    public class SharedProfile : Profile
    {
        public SharedProfile()
        {
            CreateMap<SearchQueryDTO, GetPagedJobForClientQuery>().ReverseMap();
            CreateMap<SearchQueryDTO, GetPagedJobForLawyerQuery>().ReverseMap();
        }
    }
}
