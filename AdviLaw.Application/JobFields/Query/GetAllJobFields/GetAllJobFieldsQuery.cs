using AdviLaw.Application.Basics;
using AdviLaw.Application.JobFields.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.JobFields.Query.GetAllJobFields
{
    public class GetAllJobFieldsQuery : IRequest<Response<List<JobFieldDto>>> { }
}
