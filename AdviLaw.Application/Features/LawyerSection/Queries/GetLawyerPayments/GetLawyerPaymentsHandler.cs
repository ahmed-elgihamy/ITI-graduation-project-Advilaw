using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.PaymentSection.DTOs;
using AdviLaw.Application.Features.Shared.DTOs;
using AdviLaw.Domain.Entites.PaymentSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdviLaw.Application.Features.LawyerSection.Queries.GetLawyerPayments
{
    public class GetLawyerPaymentsHandler(
            IMapper mapper,
            ResponseHandler responseHandler,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<GetLawyerPaymentsQuery, Response<PagedResponse<LawyerPaymentListDTO>>>
    {
        public async Task<Response<PagedResponse<LawyerPaymentListDTO>>> Handle(GetLawyerPaymentsQuery request, CancellationToken cancellationToken)
        {
            // Define includes (Sender and EscrowTransaction)
            List<Expression<Func<Payment, object>>> includes = new()
            {
                p => p.Sender,
                p => p.EscrowTransaction
            };

            // Base query filtered by ReceiverId and PaymentType
            var queryable = (await unitOfWork.Payments.GetAllAsync(
                filter: p =>
                    p.Type == PaymentType.SessionPayment &&
                    p.ReceiverId == request.UserId,
                includes: includes
            )).AsQueryable();

            //// Optional search filter
            //if (!string.IsNullOrWhiteSpace(request.Search))
            //{
            //    var search = request.Search.ToLower();
            //    queryable = queryable.Where(p =>
            //        p.Sender.UserName.ToLower().Contains(search) ||
            //        p.Amount.ToString().Contains(search)
            //    );
            //}

            // Total count before pagination
            int totalCount = await queryable.CountAsync(cancellationToken);

            // Apply pagination
            var pagedPayments = await queryable
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            // Map to DTOs
            var dtoList = mapper.Map<List<LawyerPaymentListDTO>>(pagedPayments);

            // Wrap in pagination response
            var pagedResponse = new PagedResponse<LawyerPaymentListDTO>(
                dtoList,
                request.PageSize,
                totalCount,
                request.PageNumber
            );

            return responseHandler.Success(pagedResponse);
        }
    }
}
