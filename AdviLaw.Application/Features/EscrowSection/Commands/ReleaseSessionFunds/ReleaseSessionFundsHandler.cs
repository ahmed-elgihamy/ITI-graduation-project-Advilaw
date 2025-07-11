// AdviLaw.Application/Features/EscrowSection/Commands/ReleaseSessionFunds/ReleaseSessionFundsHandler.cs
using AdviLaw.Application.Basics;
using AdviLaw.Domain.Entites.EscrowTransactionSection;
using AdviLaw.Domain.Entites.PaymentSection;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Configuration;
using Stripe;
using System.Linq.Expressions;
using System.Security.Cryptography.Xml;

namespace AdviLaw.Application.Features.EscrowSection.Commands.ReleaseSessionFunds
{
    public class ReleaseSessionFundsHandler : IRequestHandler<ReleaseSessionFundsCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IConfiguration _config;

        public ReleaseSessionFundsHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IConfiguration config
        )
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _config = config;
        }

        public async Task<Response<bool>> Handle(ReleaseSessionFundsCommand cmd, CancellationToken ct)
        {
            var session = await _unitOfWork.Sessions.GetByIdIncludesAsync(
                cmd.SessionId,
                includes: new List<Expression<Func<Session, object>>>
                {
                  s => s.EscrowTransaction,
                  s => s.Lawyer.User
                }
            );

            if (session == null)
                return _responseHandler.NotFound<bool>("Session not found");

            if (session.Status != SessionStatus.Completed)
                return _responseHandler.BadRequest<bool>("Session not completed yet");

            var esc = session.EscrowTransaction;
            if (esc == null || esc.Status != EscrowTransactionStatus.Completed)
                return _responseHandler.BadRequest<bool>("Escrow not completed");


            var lawyerUser = session.Lawyer.User;
            if (string.IsNullOrWhiteSpace(lawyerUser?.StripeAccountId))
                return _responseHandler.BadRequest<bool>("Lawyer Stripe account not configured");


            var secret = _config["Stripe:SecretKey"];
            if (string.IsNullOrWhiteSpace(secret))
                return _responseHandler.BadRequest<bool>("Stripe secret key missing");
            StripeConfiguration.ApiKey = secret;


            // Retrieve the Charge ID from the PaymentIntent
            var chargeService = new ChargeService();
            var charges = await chargeService.ListAsync(new ChargeListOptions
            {
                PaymentIntent = esc.TransferId
            });

            var chargeId = charges.Data.FirstOrDefault()?.Id;
            if (string.IsNullOrEmpty(chargeId))
                return _responseHandler.BadRequest<bool>("No charge found for this PaymentIntent.");

            var charge = charges.Data.FirstOrDefault();
            if (charge == null)
                return _responseHandler.BadRequest<bool>("No charge found for this PaymentIntent.");

            // Fetch the balance transaction to get the actual currency Stripe credited you
            var balanceTransactionService = new BalanceTransactionService();
            var balanceTransaction = await balanceTransactionService.GetAsync(charge.BalanceTransactionId);
            var transferCurrency = balanceTransaction.Currency; // This will be "usd" if Stripe credited you in

            // Calculate 2% commission
            var commission = Math.Round(esc.Amount * 0.02m, 2); // 2% of escrow amount

            // Amount to transfer to lawyer (98%)
            var amountToLawyer = esc.Amount - commission;

            // Stripe expects the amount in the smallest currency unit (e.g., piasters)
            var transferOptions = new TransferCreateOptions
            {
                Amount = (long)(amountToLawyer * 100), // Convert to piasters/cents
                Currency = transferCurrency,
                Destination = lawyerUser.StripeAccountId,
                TransferGroup = $"SESSION_{session.Id}",
            };
            var transferService = new TransferService();
            Transfer stripeTransfer;
            try
            {
                stripeTransfer = await transferService.CreateAsync(transferOptions);
            }
            catch (StripeException ex)
            {
                return _responseHandler.BadRequest<bool>($"Stripe transfer failed: {ex.Message}");
            }

            var payment = new Payment
            {
                Type = PaymentType.SessionPayment,
                SenderId = esc.SenderId,
                ReceiverId = lawyerUser.Id,
                EscrowTransactionId = esc.Id,
                SessionId = session.Id,
                Amount = esc.Amount
            };
            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync(ct); // This will populate payment.Id

            // 2. Assign payment ID to session
            session.PaymentId = payment.Id;
            await _unitOfWork.Sessions.UpdateAsync(session); // Make sure Update is used if you're tracking manually

            // 3. Save the updated session
            await _unitOfWork.SaveChangesAsync(ct);

            return _responseHandler.Success(true);
        }
    }
}
