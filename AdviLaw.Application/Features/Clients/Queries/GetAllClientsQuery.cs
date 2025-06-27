using MediatR;
using System.Collections.Generic;
using AdviLaw.Application.DTOs.Client;

namespace AdviLaw.Application.Features.Clients.Queries
{
    public class GetAllClientsQuery : IRequest<List<ClientListDto>>
    {
    }
} 