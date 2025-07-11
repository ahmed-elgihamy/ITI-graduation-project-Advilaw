using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.LawyerSection.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.LawyerSection.Queries.GetHourlyRate
{
    public class GetHourlyRateQuery : IRequest<Response<HourlyRateDTO>>
    {
        public int LawyerId { get; set; }
        public GetHourlyRateQuery(int lawyerId)
        {
            LawyerId = lawyerId;
        }
    }
}
