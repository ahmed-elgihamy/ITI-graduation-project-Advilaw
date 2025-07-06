using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.LawyerSection.DTOs;
using AdviLaw.Application.Features.Shared.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.LawyerSection.Queries.GetAllLawyers
{
    public class GetLawyerForAdminQuery : IRequest<Response<PagedResponse<LawyerListDTO>>>
    {
        public string? Search { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool? IsApproved { get; set; }
    }

}
