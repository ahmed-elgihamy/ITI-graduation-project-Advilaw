using AdviLaw.Application.Basics;
using AdviLaw.Domain.Entites.SubscriptionSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AdviLaw.Application.Features.UserSubscriptionSection.DTOs;

namespace AdviLaw.Application.Features.LawyerSection.Queries.GetLawyerSubscriptions
{
    public class GetLawyerSubscriptionsHandler(
            IMapper mapper,
            ResponseHandler responseHandler,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<GetLawyerSubscriptionsQuery, Response<List<LawyerSubscriptionListDTO>>>
    {
        public async Task<Response<List<LawyerSubscriptionListDTO>>> Handle(GetLawyerSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<UserSubscription, object>>>
            {
                l => l.Lawyer,
                l => l.Lawyer.User,
                l => l.SubscriptionType
            };
            var query = await unitOfWork.UserSubscriptions.GetAllAsync(
                filter: r =>
                    r.Lawyer.UserId == request.UserId
                ,
                includes: includes
            );

            var payments = await query.ToListAsync();
            var paymentsDTOs = mapper.Map<List<LawyerSubscriptionListDTO>>(payments);

            var response = responseHandler.Success(paymentsDTOs);
            return response;
        }
    }
}
