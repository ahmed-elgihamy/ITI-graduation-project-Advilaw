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

            job.Status = JobStatus.Accepted;
            await _unitOfWork.SaveChangesAsync();

            return _responseHandler.Success(true, "Consultation accepted.");
        }
    }
}