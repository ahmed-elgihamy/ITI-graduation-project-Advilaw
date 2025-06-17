using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.JobSection.Queries.GetJobByIdForClient
{
    public class GetJobByIdClientQuery : IRequest<Response<JobDetailsForClientDTO>>
    {
        public int JobId { get; set; }
        public GetJobByIdClientQuery(int jobId)
        {
            JobId = jobId;
        }
    }
}
