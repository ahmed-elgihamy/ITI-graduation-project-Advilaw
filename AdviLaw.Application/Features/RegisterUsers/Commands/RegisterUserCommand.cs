using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Application.DTOs.Users;
using MediatR;

namespace AdviLaw.Application.Features.RegisterUsers.Commands
{
    public class RegisterUserCommand : IRequest<Response<object>>
    {
        public UserRegisterDto Dto { get; set; }

        public RegisterUserCommand(UserRegisterDto dto)
        {
            Dto = dto;
        }
    }

}
