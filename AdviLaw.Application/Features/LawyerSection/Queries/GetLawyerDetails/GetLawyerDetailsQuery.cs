using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.LawyerSection.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.LawyerSection.Queries.GetLawyerDetails
{
    public class GetLawyerDetailsQuery : IRequest<Response<LawyerDetailsDTO>>
    {
        public int Id { get; set; }
        public GetLawyerDetailsQuery(int id)
        {
            Id = id;
        }
    }
}
