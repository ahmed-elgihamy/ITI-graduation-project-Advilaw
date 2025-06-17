using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.SendResetCode
{
 public   class SendResetCodeValidator:AbstractValidator<SendResetCodeCommand>
    {

        public SendResetCodeValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");


        }
    }
}
