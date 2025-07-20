using MediatR;
using AdviLaw.Application.Features.Clients.DTOs;
using System.Threading;
using System.Threading.Tasks;
using AdviLaw.Application.DTOs.Client;
using AdviLaw.Domain.UnitOfWork;

namespace AdviLaw.Application.Features.Clients.Queries.GetChat
{
    public interface IClientChatService
    {
        Task<List<ClientJobChatDto>> GetClientJobChatsAsync(string clientIdd);
    }
    public class GetClientJobChatsHandler : IRequestHandler<GetClientJobChatsQuery, List<ClientJobChatDto>>
    {
        private readonly IClientChatService _unitOfWork;

        public GetClientJobChatsHandler(IClientChatService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ClientJobChatDto>> Handle(GetClientJobChatsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetClientJobChatsAsync(request.ClientId);
        }
    }
}
