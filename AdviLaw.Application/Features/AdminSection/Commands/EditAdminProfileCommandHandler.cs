using System.Threading;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Application.Features.AdminSection.DTOs;
using AdviLaw.Domain.Enums;

namespace AdviLaw.Application.Features.AdminSection.Commands
{
    public class EditAdminProfileCommandHandler : IRequestHandler<EditAdminProfileCommand, Response<AdminListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;

        public EditAdminProfileCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }

        public async Task<Response<AdminListDto>> Handle(EditAdminProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null || user.Role != Roles.Admin)
                return _responseHandler.NotFound<AdminListDto>("Admin user not found");
                
            //if email is changed, check if it is already in use
            if (request.Dto.Email != user.Email)
            {
                var emailInUse = await _userManager.FindByEmailAsync(request.Dto.Email);
                if (emailInUse != null)
                    return _responseHandler.BadRequest<AdminListDto>("Email already in use");
            }

            // Map fields from DTO to user
            _mapper.Map(request.Dto, user);
            await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var updatedAdminDto = _mapper.Map<AdminListDto>(user);
            return new Response<AdminListDto>(updatedAdminDto, "Profile updated successfully");
        }
    }
} 