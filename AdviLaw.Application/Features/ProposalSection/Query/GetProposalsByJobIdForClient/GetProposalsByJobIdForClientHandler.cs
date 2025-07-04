using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.ProposalSection.DTOs;
using AdviLaw.Domain.Entites.ProposalSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace AdviLaw.Application.Features.ProposalSection.Query.GetProposalsByJobIdForClient
{
    public class GetProposalsByJobIdForClientHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler, IMapper mapper) : IRequestHandler<GetProposalsByJobIdForClientQuery, Response<ProposalDetails>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ResponseHandler _responseHandler = responseHandler;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<ProposalDetails>> Handle(GetProposalsByJobIdForClientQuery request, CancellationToken cancellationToken)
        {
            if (request.ProposalId <= 0)
            {
                return _responseHandler.BadRequest<ProposalDetails>("Invalid job ID.");
            }
            var proposal = await _unitOfWork.Proposals.GetByIdIncludesAsync(
                request.ProposalId,
                includes: new List<Expression<Func<Proposal, object>>>
                {
                    p => p.Lawyer,
                    p => p.Lawyer.User,
                    p => p.Job
                }
            );
            if (proposal == null)
            {
                return _responseHandler.BadRequest<ProposalDetails>("Proposal not found.");
            }
            if (proposal.Job.ClientId != request.ClientId)
            {
                return _responseHandler.BadRequest<ProposalDetails>("You do not have permission to view this proposal.");
            }
            var proposalDetails = _mapper.Map<ProposalDetails>(proposal);
            var response = _responseHandler.Success(proposalDetails, "Proposal details retrieved successfully.");
            return response;
        }
    }
}
