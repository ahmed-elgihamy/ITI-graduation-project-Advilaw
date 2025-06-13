using AdviLaw.Application.Basics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.JobFields.Command.CreateJobField
{
    public class CreateJobFieldCommand : IRequest<Response<object>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
