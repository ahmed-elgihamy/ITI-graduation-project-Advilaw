using AdviLaw.Application.Basics;
using AdviLaw.Application.JobFields.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.JobFields.Query.GetJobFieldById
{
    public class GetJobFieldByIdQuery : IRequest<Response<JobFieldDto>>
    {
        public int Id { get; }

        public GetJobFieldByIdQuery(int id)
        {
            Id = id;
        }
    }
}
