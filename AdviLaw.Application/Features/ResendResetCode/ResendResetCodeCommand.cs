using AdviLaw.Application.Basics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.ResendReset
{
    public class ResendResetCodeCommand : IRequest<Response<bool>>
    {
        public string Email { get; set; }
    }

}
