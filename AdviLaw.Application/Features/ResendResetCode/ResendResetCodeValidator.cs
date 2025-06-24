using AdviLaw.Application.Features.ResendReset;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.ResendResetCode
{
   public class ResendResetCodeValidator:AbstractValidator<ResendResetCodeCommand>
    {
        public ResendResetCodeValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");


        }
    }
}
