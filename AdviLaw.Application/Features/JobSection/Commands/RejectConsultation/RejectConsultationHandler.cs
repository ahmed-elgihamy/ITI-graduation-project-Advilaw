using AdviLaw.Application.Basics;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.UnitOfWork;
using MediatR;

namespace AdviLaw.Application.Features.JobSection.Commands.RejectConsultation
{
    public class RejectConsultationHandler : IRequestHandler<RejectConsultationCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;

        public RejectConsultationHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
        }

        public async Task<Response<bool>> Handle(RejectConsultationCommand request, CancellationToken cancellationToken)
        {
            var job = await _unitOfWork.Jobs.GetByIdAsync(request.JobId);
            if (job == null || job.Type != JobType.LawyerProposal || job.LawyerId != request.LawyerId)
                return _responseHandler.NotFound<bool>("Consultation not found or not authorized.");

            job.Status = JobStatus.Rejected;
           
            await _unitOfWork.SaveChangesAsync();

            return _responseHandler.Success(true, "Consultation rejected.");
        }
    }
}