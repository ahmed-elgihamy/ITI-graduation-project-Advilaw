using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.AdminSection.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.AdminSection.Queries
{
    public class GetAdminProfileQuery : IRequest<Response<AdminProfileDTO>>
    {
        public int AdminId { get; set; }

        public GetAdminProfileQuery(int adminId)
        {
            AdminId = adminId;
        }
    }
} 