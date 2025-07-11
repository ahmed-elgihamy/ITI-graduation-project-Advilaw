using AdviLaw.Application.Basics;
using MediatR;

namespace AdviLaw.Application.Features.EscrowSection.Queries.GetSessionHistoryForAdmin
{
    public class GetSessionHistoryForAdminQuery : IRequest<Response<List<SessionHistoryForAdminDto>>>
    {
       
    }
} 