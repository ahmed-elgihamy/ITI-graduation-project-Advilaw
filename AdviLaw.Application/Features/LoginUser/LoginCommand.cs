using AdviLaw.Application.DTOs.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.LoginUser
{
  public class LoginCommand:IRequest<AuthResponse>
    {

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
