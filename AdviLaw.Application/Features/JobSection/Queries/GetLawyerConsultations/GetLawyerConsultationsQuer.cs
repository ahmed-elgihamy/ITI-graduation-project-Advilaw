using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Application.Features.Shared.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.JobSection.Queries.GetLawyerConsultations
{
    public class GetLawyerConsultationsQuery : IRequest<Response<PagedResponse<JobListDTO>>>
    {
        public int LawyerId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}