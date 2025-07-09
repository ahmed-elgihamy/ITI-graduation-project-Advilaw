using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Application.Features.Shared.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.JobSection.Queries.GetClientConsultations
{
    public class GetClientConsultationsQuery : IRequest<Response<PagedResponse<ClientConsultationDTO>>>
    {
        public int ClientId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}