using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Reviews.DTOs;
using AdviLaw.Application.Features.Shared.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.LawyerSection.Queries.GetLawyerReviews
{
    public class GetLawyerReviewsQuery(string userId, string? search = null, int? pageNumber = 1, int? pageSize = 10) : IRequest<Response<PagedResponse<LawyerReviewListDTO>>>
    {
        public string UserId { get; set; } = userId;
        public string? Search { get; set; } = search;
        public int PageNumber { get; set; } = pageNumber ?? 1;
        public int PageSize { get; set; } = pageSize ?? 10;
    }
}
