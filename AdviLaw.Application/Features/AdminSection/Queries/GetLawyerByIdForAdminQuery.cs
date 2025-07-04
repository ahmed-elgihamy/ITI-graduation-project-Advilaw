using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.AdminSection.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.AdminSection.Queries
{
    public class GetLawyerByIdForAdminQuery : IRequest<Response<LawyerForAdminDto>>
    {
        public int Id { get; set; }

        public GetLawyerByIdForAdminQuery(int id)
        {
            Id = id;
        }
    }
}
