using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.EscrowSection.Commands.ConfirmSessionPayment;
using AdviLaw.Application.Features.EscrowSection.DTOs;
using AdviLaw.Domain.Entites.EscrowTransactionSection;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entites.PaymentSection;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;

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

            //strore the paymentIntentId from Stripe session
            escrow.TransferId = stripeSession.PaymentIntentId;

            escrow.Status = EscrowTransactionStatus.Completed;


            // Store the PaymentIntentId from Stripe session
            escrow.TransferId = stripeSession.PaymentIntentId;


            var job = await _unitOfWork.Jobs.GetByIdAsync(escrow.JobId);
            if (job != null && job.Status == JobStatus.WaitingPayment)
            {
                job.Status = JobStatus.Started;
            }

            // If no session linked, create one
            if (escrow.SessionId == null)
            {
                // You may need to fetch job, client, lawyer info as needed
                var jobForSession = await _unitOfWork.Jobs.GetByIdAsync(escrow.JobId);
                if (jobForSession == null)
                    return _responseHandler.BadRequest<int>("Job not found for escrow.");

                var jobField = await _unitOfWork.JobFields.GetByIdAsync(jobForSession.JobFieldId);
                if (jobField == null)
                    return _responseHandler.BadRequest<int>("JobField not found for job.");

                var session = new Domain.Entites.SessionSection.Session
                {
                    JobId = jobForSession.Id,
                    Job = null,
                    ClientId = jobForSession.ClientId ?? 0,
                    LawyerId = jobForSession.LawyerId ?? 0,
                    EscrowTransactionId = escrow.Id,
                    Status = SessionStatus.ClientReady
                };

                await _unitOfWork.Sessions.AddAsync(session);
                await _unitOfWork.SaveChangesAsync(ct);

                escrow.SessionId = session.Id;
            }

            await _unitOfWork.SaveChangesAsync(ct);

            // Check if SessionId is still null after all processing
            if (!escrow.SessionId.HasValue)
            {
                return _responseHandler.BadRequest<int>("Failed to create or retrieve session ID");
            }

            return _responseHandler.Success(escrow.SessionId.Value);

        }

    }

}



