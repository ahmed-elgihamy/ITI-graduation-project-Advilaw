using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Application.DTOs.Client;
using MediatR;

namespace AdviLaw.Application.Features.AdminSection.Queries
{
    public class GetClientByIdForAdminQuery : IRequest<Response<ClientListDto>>
    {
        public int Id { get; set; }

        public GetClientByIdForAdminQuery(int id)
        {
            Id = id;
        }
    }
}
