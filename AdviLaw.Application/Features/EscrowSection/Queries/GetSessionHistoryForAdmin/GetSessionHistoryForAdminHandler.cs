using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.EscrowSection.Queries.GetSessionHistoryForAdmin;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entites.EscrowTransactionSection;
using AdviLaw.Domain.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdviLaw.Application.Features.EscrowSection.Queries.GetSessionHistoryForAdmin
{
    public class GetSessionHistoryForAdminHandler : IRequestHandler<GetSessionHistoryForAdminQuery, Response<List<SessionHistoryForAdminDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;

        public GetSessionHistoryForAdminHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
        }

        public async Task<Response<List<SessionHistoryForAdminDto>>> Handle(GetSessionHistoryForAdminQuery request, CancellationToken cancellationToken)
        {
            var sessions = await _unitOfWork.Sessions
                .GetAllAsync()
                .Result
                .Include(s => s.Job)
                .Include(s => s.Lawyer)
                .Include(s => s.Client)
                .Include(s => s.EscrowTransaction)
                .Where(s => s.Status == SessionStatus.Completed && 
                           s.EscrowTransaction.Status == EscrowTransactionStatus.Completed &&
                           s.PaymentId != null)
                .OrderByDescending(s => s.EscrowTransaction.ReleasedAt)
                .Select(s => new SessionHistoryForAdminDto
                {
                    Id = s.Id,
                    SessionId = s.Id,
                    JobTitle = s.Job.Header,
                    LawyerName = s.Lawyer.User.UserName,
                    ClientName = s.Client.User.UserName,
                    Amount = s.EscrowTransaction.Amount,
                    Status = s.Status.ToString(),
                    CreatedAt = s.EscrowTransaction.CreatedAt,
                    PaymentId = s.PaymentId ?? 0,
                    PaymentCreatedAt = s.EscrowTransaction.ReleasedAt
                })
                .ToListAsync(cancellationToken);

            return _responseHandler.Success(sessions);
        }
    }
} 