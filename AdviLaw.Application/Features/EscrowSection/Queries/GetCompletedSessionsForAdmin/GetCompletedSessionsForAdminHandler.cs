using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.EscrowSection.Queries.GetCompletedSessionsForAdmin;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entites.EscrowTransactionSection;
using AdviLaw.Domain.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdviLaw.Application.Features.EscrowSection.Queries.GetCompletedSessionsForAdmin
{
    public class GetCompletedSessionsForAdminHandler : IRequestHandler<GetCompletedSessionsForAdminQuery, Response<List<CompletedSessionForAdminDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;

        public GetCompletedSessionsForAdminHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
        }

        public async Task<Response<List<CompletedSessionForAdminDto>>> Handle(GetCompletedSessionsForAdminQuery request, CancellationToken cancellationToken)
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
                           s.PaymentId == null)
                .OrderByDescending(s => s.EscrowTransaction.CreatedAt)
                .Select(s => new CompletedSessionForAdminDto
                {
                    Id = s.Id,
                    SessionId = s.Id,
                    JobTitle = s.Job.Header,
                    LawyerName = s.Lawyer.User.UserName,
                    ClientName = s.Client.User.UserName,
                    Amount = s.EscrowTransaction.Amount,
                    Status = s.Status.ToString(),
                    CreatedAt = s.EscrowTransaction.CreatedAt,
                    CompletedAt = s.EscrowTransaction.ReleasedAt
                })
                .ToListAsync(cancellationToken);

            return _responseHandler.Success(sessions);
        }
    }
} 