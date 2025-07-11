using AdviLaw.Application.Basics;
using MediatR;

namespace AdviLaw.Application.Features.EscrowSection.Queries.GetCompletedSessionsForAdmin
{
    public class GetCompletedSessionsForAdminQuery : IRequest<Response<List<CompletedSessionForAdminDto>>>
    {
       
    }
} 