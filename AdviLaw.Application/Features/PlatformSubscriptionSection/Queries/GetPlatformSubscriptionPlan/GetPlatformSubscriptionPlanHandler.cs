using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs;
using AdviLaw.Domain.Entites.SubscriptionSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Queries.GetPlatformSubscriptionPlan
{
    public class GetPlatformSubscriptionPlanHandler(IMapper mapper,
        IUnitOfWork unitOfWork,
        ResponseHandler responseHandler,
        IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetPlatformSubscriptionPlanQuery, Response<List<PlatformSubscriptionDTO>>>
    {
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly ResponseHandler _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        public async Task<Response<List<PlatformSubscriptionDTO>>> Handle(GetPlatformSubscriptionPlanQuery request, CancellationToken cancellationToken)
        {
            var query = await unitOfWork.PlatformSubscriptions.GetAllAsync(
                filter: ps => ps.IsActive == true,
                includes:
                    new List<Expression<Func<PlatformSubscription, object>>>
                    {
                        j => j.Details,
                    }
            );
            var subscriptions = await query.ToListAsync();
            var dto = _mapper.Map<List<PlatformSubscriptionDTO>>(subscriptions);
            var response = _responseHandler.Success(dto, "Job details retrieved successfully.");
            return response;
        }
    }
}

