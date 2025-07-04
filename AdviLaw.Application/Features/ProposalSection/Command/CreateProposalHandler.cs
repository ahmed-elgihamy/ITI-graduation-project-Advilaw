using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Application.Features.ProposalSection.DTOs;
using AdviLaw.Domain.Entites.ProposalSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;

namespace AdviLaw.Application.Features.ProposalSection.Command
{
    public class CreateProposalHandler(
        IMapper mapper,
        ResponseHandler responseHandler,
        IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateProposalCommand, Response<CreatedProposalDTO>>
    {
        public async Task<Response<CreatedProposalDTO>> Handle(CreateProposalCommand request, CancellationToken cancellationToken)
        {
            var lawyerAlreadyMadeProposal = await unitOfWork.Proposals.FindFirstAsync(p=>p.LawyerId == request.LawyerId && p.JobId == request.JobId);
            if ( lawyerAlreadyMadeProposal != null )
            {
                return responseHandler.BadRequest<CreatedProposalDTO>("You can't make more than one proposal.");
            }
            var mappedProposal = mapper.Map<Proposal>( request );
            var createdProposal = await unitOfWork.Proposals.AddAsync(mappedProposal);
            await unitOfWork.SaveChangesAsync();
            var mappedCreatedProposal = mapper.Map<CreatedProposalDTO>(createdProposal);
            var response = responseHandler.Success(mappedCreatedProposal);
            return response;
        }
    }
}