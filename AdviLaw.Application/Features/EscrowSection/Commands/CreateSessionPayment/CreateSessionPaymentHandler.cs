using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.EscrowSection.Commands.CreateSessionPayment;
using AdviLaw.Application.Features.EscrowSection.DTOs;
using AdviLaw.Domain.Entites.EscrowTransactionSection;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.UnitOfWork;
using MediatR;

namespace AdviLaw.Application.Features.EscrowSection.Commands.CreateSessionPayment
{
    public class CreateSessionPaymentHandler
        : IRequestHandler<CreateSessionPaymentCommand, Response<CreatedEscrowDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;

        public CreateSessionPaymentHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
        }

        public async Task<Response<CreatedEscrowDTO>> Handle(CreateSessionPaymentCommand cmd, CancellationToken ct)
        {

            var job = await _unitOfWork.Jobs.GetByIdAsync(cmd.JobId);
            if (job == null)
                return _responseHandler.BadRequest<CreatedEscrowDTO>("Job not found");


            if (job.Status != JobStatus.WaitingPayment)
                return _responseHandler.BadRequest<CreatedEscrowDTO>("Job is not in payment status");

            // Check for existing escrow
            var existingEscrow = await _unitOfWork.Escrows.FindFirstAsync(e => e.JobId == cmd.JobId);
            if (existingEscrow != null)
            {
                var existingDto = new CreatedEscrowDTO
                {
                    EscrowId = existingEscrow.Id,
                    Amount = existingEscrow.Amount,
                    Currency = existingEscrow.Currency.ToString().ToLower(),
                    CreatedAt = existingEscrow.CreatedAt
                };
                return _responseHandler.Success(existingDto);
            }

            var escrowTransaction = new EscrowTransaction
            {
                JobId = cmd.JobId,
                SenderId = cmd.ClientId,
                Amount = job.Budget,
                Currency = CurrencyType.EGP,
                Status = EscrowTransactionStatus.Pending,
                Type = EscrowTransactionType.ClientTransaction,
                PaymentMethod = PaymentMethod.Stripe
            };

            await _unitOfWork.Escrows.AddAsync(escrowTransaction);
            await _unitOfWork.SaveChangesAsync(ct);


            var dto = new CreatedEscrowDTO
            {
                EscrowId = escrowTransaction.Id,
                Amount = escrowTransaction.Amount,
                Currency = escrowTransaction.Currency.ToString().ToLower(),
                CreatedAt = escrowTransaction.CreatedAt
            };

            return _responseHandler.Success(dto);
        }
    }
}
