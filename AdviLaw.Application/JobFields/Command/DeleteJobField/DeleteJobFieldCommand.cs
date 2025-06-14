using AdviLaw.Application.Basics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.JobFields.Command.DeleteJobField
{
    public class DeleteJobFieldCommand : IRequest<Response<object>>
    {
        public DeleteJobFieldCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
