using System.Threading;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Enums;

namespace AdviLaw.Application.Features.AdminSection.Commands
{
    public class AssignAdminRoleCommandHandler : IRequestHandler<AssignAdminRoleCommand, Response<object>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        public AssignAdminRoleCommandHandler(UserManager<User> userManager, IUnitOfWork unitOfWork, ResponseHandler responseHandler)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
        }
        public async Task<Response<object>> Handle(AssignAdminRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null || (user.Role != Roles.Admin && user.Role != Roles.SuperAdmin))
                return _responseHandler.NotFound<object>("Admin user not found");

            if (!System.Enum.TryParse<Roles>(request.Role, out var newRole) || (newRole != Roles.Admin && newRole != Roles.SuperAdmin))
                return _responseHandler.BadRequest<object>("Invalid role. Only Admin or SuperAdmin allowed.");
            user.Role = newRole;
            await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _responseHandler.Success<object>($"Admin role updated to {newRole}");
        }
    }
} 
