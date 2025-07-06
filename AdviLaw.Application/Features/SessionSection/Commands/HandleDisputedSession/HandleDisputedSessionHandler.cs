using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.SessionSection.Commands.HandleDisputedSession;
using AdviLaw.Application.Features.SessionSection.DTOs;
using AdviLaw.Domain.Entites.EscrowTransactionSection;
using AdviLaw.Domain.Entites.PaymentSection;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace AdviLaw.Application.Features.SessionSection.Commands.HandleDisputedSession
{
    public class HandleDisputedSessionHandler : IRequestHandler<HandleDisputedSessionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;

        public HandleDisputedSessionHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
        }

        public async Task<Response<bool>> Handle(HandleDisputedSessionCommand cmd, CancellationToken ct)
        {
            var session = await _unitOfWork.Sessions.FindFirstAsync(
                s => s.Id == cmd.SessionId,
                new List<Expression<Func<Session, object>>>
                {
                    s => s.Client.User,
                    s => s.Lawyer.User,
                    s => s.EscrowTransaction
                });

            if (session == null)
                return _responseHandler.NotFound<bool>("Session not found");

            var escrow = session.EscrowTransaction;
            if (escrow == null || escrow.Status != EscrowTransactionStatus.Completed)
                return _responseHandler.BadRequest<bool>("Funds not yet released to escrow");

            decimal amount = escrow.Amount;

            if (cmd.CausedBy.ToLower() == "client")
            {
                decimal refundAmount = amount * 0.9m;

                var payment = new Payment
                {
                    Type = PaymentType.RefundPayment,
                    SenderId = session.Client.UserId,
                    ReceiverId = session.Client.UserId,
                    Amount = refundAmount,
                    SessionId = session.Id,
                    EscrowTransactionId = escrow.Id
                };

                await _unitOfWork.Payments.AddAsync(payment);
            }
            else if (cmd.CausedBy.ToLower() == "lawyer")
            {
                var payment = new Payment
                {
                    Type = PaymentType.RefundPayment,
                    SenderId = session.Lawyer.UserId,
                    ReceiverId = session.Client.UserId,
                    Amount = amount,
                    SessionId = session.Id,
                    EscrowTransactionId = escrow.Id
                };

                await _unitOfWork.Payments.AddAsync(payment);


                var subscription = await _unitOfWork.UserSubscriptions.FindFirstAsync(
                    s => s.LawyerId == session.LawyerId && s.IsActive);

                if (subscription != null)
                    subscription.IsActive = false;
            }
            else
            {
                return _responseHandler.BadRequest<bool>("Invalid CausedBy. Use 'client' or 'lawyer'");
            }

            session.Status = SessionStatus.Refunded;

            await _unitOfWork.SaveChangesAsync(ct);

            return _responseHandler.Success(true);
        }
    }
}
