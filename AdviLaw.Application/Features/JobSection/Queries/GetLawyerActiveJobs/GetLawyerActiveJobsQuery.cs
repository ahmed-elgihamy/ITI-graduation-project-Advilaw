using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Application.Features.Shared.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.JobSection.Queries.GetLawyerActiveJobs
{
    public class GetLawyerActiveJobsQuery : IRequest<Response<PagedResponse<JobListForLawyerDTO>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; } = null;
        public int LawyerId { get; set; }
    }
}
