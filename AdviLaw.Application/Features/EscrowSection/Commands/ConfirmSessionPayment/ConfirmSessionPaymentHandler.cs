using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.EscrowSection.Commands.ConfirmSessionPayment;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;
using AdviLaw.Domain.Entites.EscrowTransactionSection;

namespace AdviLaw.Application.Features.EscrowSection.Commands.ConfirmSessionPayment
{
    public class ConfirmSessionPaymentHandler
        : IRequestHandler<ConfirmSessionPaymentCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IConfiguration _config;

        public ConfirmSessionPaymentHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _config = config;
        }

        public async Task<Response<int>> Handle(ConfirmSessionPaymentCommand cmd, CancellationToken ct)
        {
            var secretKey = _config["Stripe:SecretKey"];
            if (string.IsNullOrWhiteSpace(secretKey))
                return _responseHandler.BadRequest<int>("Stripe key not configured");

            Stripe.StripeConfiguration.ApiKey = secretKey;

            var service = new SessionService();
            var stripeSession = await service.GetAsync(cmd.StripeSessionId);
            if (stripeSession.PaymentStatus != "paid")
                return _responseHandler.BadRequest<int>("Payment not completed");

            // Find escrow by StripeSessionId
            var escrow = await _unitOfWork.Escrows.FindFirstAsync(e => e.StripeSessionId == cmd.StripeSessionId);
            if (escrow == null)
                return _responseHandler.NotFound<int>("Escrow not found");

            // Store the PaymentIntentId from the Stripe session
            escrow.TransferId = stripeSession.PaymentIntentId;

            escrow.Status = EscrowTransactionStatus.Completed;

            // If no session linked, create one
            if (escrow.SessionId == null)
            {
                // You may need to fetch job, client, lawyer info as needed
                var job = await _unitOfWork.Jobs.GetByIdAsync(escrow.JobId);
                if (job == null)
                    return _responseHandler.BadRequest<int>("Job not found for escrow.");

                var jobField = await _unitOfWork.JobFields.GetByIdAsync(job.JobFieldId);
                if (jobField == null)
                    return _responseHandler.BadRequest<int>("JobField not found for job.");

                var session = new AdviLaw.Domain.Entites.SessionSection.Session
                {
                    JobId = job.Id,
                    Job = null,
                    ClientId = job.ClientId ?? 0,
                    LawyerId = job.LawyerId ?? 0,
                    EscrowTransactionId = escrow.Id,
                    Status = SessionStatus.ClientReady
                };

                await _unitOfWork.Sessions.AddAsync(session);
                await _unitOfWork.SaveChangesAsync(ct);

                escrow.SessionId = session.Id;
            }

            await _unitOfWork.SaveChangesAsync(ct);

            return _responseHandler.Success(escrow.SessionId.Value);
        }
    }
}
