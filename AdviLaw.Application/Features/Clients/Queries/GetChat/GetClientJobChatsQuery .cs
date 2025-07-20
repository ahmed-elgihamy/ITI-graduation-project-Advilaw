using AdviLaw.Application.DTOs.Client;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Clients.Queries.GetChat
{
    public class GetClientJobChatsQuery : IRequest<List<ClientJobChatDto>>
    {
        public string ClientId { get; set; }

        public GetClientJobChatsQuery(string clientId)
        {
            ClientId = clientId;
        }
    }

}
