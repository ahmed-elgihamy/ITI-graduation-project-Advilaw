using AdviLaw.Application.Features.SessionSection.DTOs;
using Azure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.SessionSection.Query
{
    public class GetSessionDetailsQuery : IRequest<SessionDetailsDto>
    {
    
        public int SessionId { get; set; }

        public GetSessionDetailsQuery( int id)
        {
            SessionId = id;
        }
    }
}
