using MediatR;
using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.AdminSection.DTOs;

namespace AdviLaw.Application.Features.AdminSection.Commands
{
    public class AssignAdminRoleCommand : IRequest<Response<object>>
    {
        public string UserId { get; set; }
        public string Role { get; set; }
        public AssignAdminRoleCommand(string userId, string role)
        {
            UserId = userId;
            Role = role;
        }
    }
} 