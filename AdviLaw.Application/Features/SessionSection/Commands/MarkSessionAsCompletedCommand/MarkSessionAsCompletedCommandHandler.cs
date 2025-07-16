using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Domain.Repositories;
using MediatR;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.UnitOfWork;

namespace AdviLaw.Application.Features.SessionSection.Commands.MarkSessionAsCompletedCommand
{
    public class MarkSessionAsCompletedCommandHandler: IRequestHandler<MarkSessionAsCompletedCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public MarkSessionAsCompletedCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(MarkSessionAsCompletedCommand request, CancellationToken cancellationToken)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(request.SessionId);
            if (session == null)
                return false;
            session.Status = SessionStatus.Completed;
            await _unitOfWork.Sessions.UpdateAsync(session);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
