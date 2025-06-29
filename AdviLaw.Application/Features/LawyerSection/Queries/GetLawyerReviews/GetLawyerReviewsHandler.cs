using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Reviews.DTOs;
using AdviLaw.Application.Features.Shared.DTOs;
using AdviLaw.Domain.Entites.SessionUtilities.ReviewSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdviLaw.Application.Features.LawyerSection.Queries.GetLawyerReviews
{
    public class GetLawyerReviewsHandler(
            IMapper mapper,
            ResponseHandler responseHandler,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<GetLawyerReviewsQuery, Response<PagedResponse<LawyerReviewListDTO>>>
    {
        public async Task<Response<PagedResponse<LawyerReviewListDTO>>> Handle(GetLawyerReviewsQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Review, object>>>
            {
                l => l.Reviewer,
                l => l.Reviewee
            };
            var query = await unitOfWork.Reviews.GetAllAsync(
                filter: r =>
                    r.ReviewerId == request.UserId ||
                    r.RevieweeId == request.UserId
                ,
                includes: includes
            );


            var totalCount = await query.CountAsync(cancellationToken);

            var pagedReviews = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var reviewsDTOs = mapper.Map<List<LawyerReviewListDTO>>(pagedReviews);

            var pagedResponse = new PagedResponse<LawyerReviewListDTO>(
                reviewsDTOs, request.PageSize, totalCount, request.PageNumber
            );
            var response = responseHandler.Success(pagedResponse);
            return response;
        }
    }
}
