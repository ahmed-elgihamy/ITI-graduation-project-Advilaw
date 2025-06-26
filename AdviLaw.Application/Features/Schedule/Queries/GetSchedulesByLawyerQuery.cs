using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Schedule.DTOs;
using MediatR;
using System.Collections.Generic;

namespace AdviLaw.Application.Features.Schedule.Queries
{
    public class GetSchedulesByLawyerQuery : IRequest<Response<List<ScheduleDTO>>>
    {
        public int LawyerId { get; set; }

        public GetSchedulesByLawyerQuery(int lawyerId)
        {
            LawyerId = lawyerId;
        }
    }
}
