using AdviLaw.Application.Common;
using AdviLaw.Application.Features.Lawyers.Queries.DTOs;
using AdviLaw.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Lawyers.Queries.GetAllLawyers
{
    public class GetAllLawyersQuery : IRequest<PagedResult<LawyerListItemDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
