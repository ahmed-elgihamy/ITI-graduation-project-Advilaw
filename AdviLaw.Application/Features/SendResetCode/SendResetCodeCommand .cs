using AdviLaw.Application.Basics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.SendResetCode
{
    public class SendResetCodeCommand : IRequest<Response<bool>>
    {
        public string Email { get; set; } = null!;
    }
}
