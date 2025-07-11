using AdviLaw.Application.Basics;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.UnitOfWork;
using MediatR;

namespace AdviLaw.Application.Features.JobSection.Commands.AcceptConsultation
{
    public class AcceptConsultationHandler : IRequestHandler<AcceptConsultationCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;

        public AcceptConsultationHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
        }

        public async Task<Response<bool>> Handle(AcceptConsultationCommand request, CancellationToken cancellationToken)
        {
            var job = await _unitOfWork.Jobs.GetByIdAsync(request.JobId);
            if (job == null || job.Type != JobType.LawyerProposal || job.LawyerId != request.LawyerId)
                return _responseHandler.NotFound<bool>("Consultation not found or not authorized.");

            // Ensure Client is loaded
            if (job.Client == null && job.ClientId.HasValue)
            {
                job.Client = await _unitOfWork.Clients.GetByIdAsync(job.ClientId.Value);
            }

            job.Status = JobStatus.WaitingPayment;

            // منطق ربط الاستشارة بالـ Escrow
            var existingEscrow = await _unitOfWork.Escrows.FindFirstAsync(e => e.JobId == job.Id);
            if (existingEscrow == null)
            {
                var escrow = new AdviLaw.Domain.Entites.EscrowTransactionSection.EscrowTransaction
                {
                    JobId = job.Id,
                    Amount = job.Budget,
                    Currency = AdviLaw.Domain.Entites.EscrowTransactionSection.CurrencyType.EGP,
                    Status = AdviLaw.Domain.Entites.EscrowTransactionSection.EscrowTransactionStatus.Pending,
                    SenderId = job.Client?.UserId, // Use the string UserId for FK
                    CreatedAt = DateTime.UtcNow
                };
                await _unitOfWork.Escrows.AddAsync(escrow);
            }

            await _unitOfWork.SaveChangesAsync();

            return _responseHandler.Success(true, "Consultation accepted.");
        }
    }
}