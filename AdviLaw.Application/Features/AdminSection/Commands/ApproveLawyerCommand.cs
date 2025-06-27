using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using MediatR;

namespace AdviLaw.Application.Features.AdminSection.Commands
{
    public class ApproveLawyerCommand :IRequest<Response<object>>
    {
        public int lawyerId { get; set; }
        public ApproveLawyerCommand(int id)
        { this.lawyerId = id;}
    }
}
