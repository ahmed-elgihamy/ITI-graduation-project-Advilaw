using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AdviLaw.Application.Features.AdminSection.DTOs;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdviLaw.Application.Features.AdminSection.Queries
{
    public class GetAllAdminsQueryHandler : IRequestHandler<GetAllAdminsQuery, List<AdminListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllAdminsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public async Task<List<AdminListDto>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
        {
            var admins = await _unitOfWork.GenericAdmins.GetAllAsync(null, null, new List<Expression<System.Func<Admin, object>>> { a => a.User });
            var adminList = admins.Select(a => new AdminListDto
            {
                Id = a.Id.ToString(),
                UserId = a.User.Id,
                UserName = a.User.UserName,
                Email = a.User.Email,
                Role = a.User.Role.ToString(),
                CreatedAt = a.User.CreatedAt
            }).ToList();
            return adminList;
        }
    }
} 