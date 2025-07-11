using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Schedule.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.Schedule.Queries.GetScheduleByLawyerIdNormal
{
    public class GetSchedulesByLawyerNormalQuery : IRequest<Response<List<CreatedScheduleDTO>>>
    {
        public Guid LawyerId { get; }

        public GetSchedulesByLawyerNormalQuery(Guid lawyerId)
        {
            LawyerId = lawyerId;
        }
    }

}

