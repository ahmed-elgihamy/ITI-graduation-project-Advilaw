using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.JobSection.Queries.GetJobByIdForLawyer
{
    public class GetJobByIdLawyerQuery : IRequest<Response<JobDetailsForLawyerDTO>>
    {
        public int JobId { get; set; }
        public GetJobByIdLawyerQuery(int jobId)
        {
            JobId = jobId;
        }
    }
}
