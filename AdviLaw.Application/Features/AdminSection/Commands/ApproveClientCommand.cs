using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using MediatR;

namespace AdviLaw.Application.Features.AdminSection.Commands
{
    public class ApproveClientCommand:IRequest<Response<object>>
    {
        public int ClientId { get; set; }

        public ApproveClientCommand(int clientId)
        {
            ClientId = clientId;
        }
    }
}
