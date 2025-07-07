using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Schedule.DTOs;
using AdviLaw.Application.Features.ScheduleSection.DTOs;
using MediatR;
using System.Collections.Generic;

namespace AdviLaw.Application.Features.Schedule.Queries
{
    public class GetSchedulesByLawyerQuery : IRequest<Response<List<LawyerScheduleDTO>>>
    {
        public Guid LawyerId { get; }

        public GetSchedulesByLawyerQuery(Guid lawyerId)
        {
            LawyerId = lawyerId;
        }
    }

}

