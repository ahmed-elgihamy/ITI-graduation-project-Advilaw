using AdviLaw.Application.Basics;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entites.ProposalSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace AdviLaw.Application.Features.ProposalSection.Command.AcceptProposal
{
    public class AcceptProposalHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler, IMapper mapper) : IRequestHandler<AcceptProposalCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly ResponseHandler _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<bool>> Handle(AcceptProposalCommand request, CancellationToken cancellationToken)
        {
            if (request.ProposalId <= 0)
            {
                return _responseHandler.BadRequest<bool>("Invalid proposal ID.");
            }
            var proposal = await _unitOfWork.Proposals.GetByIdIncludesAsync(
                request.ProposalId,
                includes: new List<Expression<Func<Proposal, object>>>
                {
                    p => p.Job,
                }
            );
            if (proposal == null)
            {
                return _responseHandler.NotFound<bool>("Proposal not found.");
            }
            if (proposal.Job.ClientId != request.ClientId)
            {
                return _responseHandler.Unauthorized<bool>("You do not have permission to accept this proposal.");
            }
            if (proposal.Job.Status != JobStatus.NotAssigned)
            {
                return _responseHandler.BadRequest<bool>("Job is not in a valid state to accept proposals.");
            }

            proposal.Status = ProposalStatus.Accepted;
            proposal.Job.Status = JobStatus.WaitingAppointment;
            proposal.Job.LawyerId = proposal.LawyerId;

            var proposals = await _unitOfWork.Proposals.GetAllAsync(
                filter: p =>
                    p.JobId == proposal.JobId &&
                    p.Status == ProposalStatus.None &&
                    p.Id != proposal.Id
            );
            foreach (var p in proposals)
            {
                p.Status = ProposalStatus.Rejected;
            }
            //await _unitOfWork.Proposals.UpdateAsync(proposal);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                return _responseHandler.Success(true, "Proposal accepted successfully.");
            }
            var response = _responseHandler.BadRequest<bool>("Failed to accept the proposal.");
            return response;
        }
    }
}
