using MediatR;
using System.Collections.Generic;
using AdviLaw.Application.Features.AdminSection.DTOs;

namespace AdviLaw.Application.Features.AdminSection.Queries
{
    public class GetAllAdminsQuery : IRequest<List<AdminListDto>>
    {
    }
} 