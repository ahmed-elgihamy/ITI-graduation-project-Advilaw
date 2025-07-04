using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.AdminSection.DTOs;
using AdviLaw.Application.Features.LawyerSection.DTOs;
using AdviLaw.Application.Features.Shared.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.AdminSection.Queries
{
    public class GetLawyerForAdminQuery: IRequest<Response<PagedResponse<LawyerForAdminDto>>>
    {
        public string? Search { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool? IsApproved { get; set; }

    }
}
