using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.SessionSection.DTOs;
using AdviLaw.Domain.UnitOfWork;
using MediatR;

namespace AdviLaw.Application.Features.SessionSection.Query
{
    public class GetSessionDetailsHandler : IRequestHandler<GetSessionDetailsQuery, SessionDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSessionDetailsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SessionDetailsDto> Handle(GetSessionDetailsQuery request, CancellationToken cancellationToken)
        {
            var session = await _unitOfWork.Sessions.GetSessionWithClientAndLawyerAsync(request.SessionId);

            if (session == null)
                throw new KeyNotFoundException($"Session with ID {request.SessionId} not found");

            return new SessionDetailsDto
            {
                SessionId = session.Id,
                JobHeader = session.Job?.Header ?? "",
                Budget = session.Job?.Budget ?? 0,
                LawyerName = session.Lawyer?.User?.UserName ?? "N/A",
                ClientName = session.Client?.User?.UserName ?? "N/A",
                AppointmentTime = session.Job?.AppointmentTime ?? DateTime.MinValue,
                DurationHours = session.Job?.DurationHours ?? 0,
                CreatedAt = session.Job?.CreatedAt ?? DateTime.MinValue,
                ClientId =session.Client.UserId,
                LawyerId=session.Lawyer.UserId

                
            };
        }
    }}
