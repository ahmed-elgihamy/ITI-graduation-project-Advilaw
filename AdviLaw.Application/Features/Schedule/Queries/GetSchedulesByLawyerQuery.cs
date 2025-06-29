using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Schedule.DTOs;
using MediatR;
using System.Collections.Generic;

namespace AdviLaw.Application.Features.Schedule.Queries
{
    public class GetSchedulesByLawyerQuery : IRequest<Response<List<LawyerScheduleDTO>>>
    {
        public int LawyerId { get; }

        public GetSchedulesByLawyerQuery(int lawyerId)
        {
            LawyerId = lawyerId;
        }
    }
}

