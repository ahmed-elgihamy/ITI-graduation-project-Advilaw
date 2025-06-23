using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Application.Features.Shared.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.JobSection.Queries.GetPagedJobs
{
    public class GetPagedJobForClientQuery : IRequest<Response<PagedResponse<JobListForClientDTO>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; } = null;
        public int ClientId { get; set; } = 0;
    }
}
