using AdviLaw.Application.Features.SessionSection.Commands.MarkSessionAsCompleted;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Enums; // Assuming you have an enum for session statuses
using AdviLaw.Domain.Repositories;
using MediatR;

namespace AdviLaw.Application.Features.SessionSection.Commands
{
    public class MarkSessionAsCompletedCommandHandler : IRequestHandler<MarkSessionAsCompletedCommand, bool>
    {
        private readonly ISessionRepository _sessionRepository;

        public MarkSessionAsCompletedCommandHandler(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<bool> Handle(MarkSessionAsCompletedCommand request, CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.GetByIdAsync(request.SessionId);

            if (session == null)
                return false;

            session.Status = SessionStatus.Completed; // Or use: SessionStatus.Completed;

            await _sessionRepository.UpdateAsync(session, cancellationToken);
            return true;
        }
    }
}
