using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.EscrowSection.Commands.ConfirmSessionPayment;
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
            var session = await service.GetAsync(cmd.SessionId);
            if (session.PaymentStatus != "paid")
                return _responseHandler.BadRequest<int>("Payment not completed");

            if (!session.Metadata.TryGetValue("EscrowId", out var escIdStr)
             || !int.TryParse(escIdStr, out var escId))
                return _responseHandler.BadRequest<int>("Escrow ID missing");

            var escrow = await _unitOfWork.Escrows.GetByIdAsync(escId);
            if (escrow == null)
                return _responseHandler.NotFound<int>("Escrow not found");

            escrow.Status = Domain.Entites.EscrowTransactionSection.EscrowTransactionStatus.Completed;

            if (escrow.SessionId == null)
                return _responseHandler.BadRequest<int>("No session linked to this escrow");

            await _unitOfWork.SaveChangesAsync(ct);

            return _responseHandler.Success(escrow.SessionId.Value);
        }
    }
}
