using MediatR;
using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.AdminSection.DTOs;

namespace AdviLaw.Application.Features.AdminSection.Commands
{
    public class EditAdminProfileCommand : IRequest<Response<AdminListDto>>
    {
        public EditAdminProfileDto Dto { get; set; }
        public string UserId { get; set; } // The ID of the admin user to edit
        public EditAdminProfileCommand(EditAdminProfileDto dto, string userId)
        {
            Dto = dto;
            UserId = userId;
        }
    }
} 