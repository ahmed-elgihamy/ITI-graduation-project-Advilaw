using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.ResetPassword
{
   public class ResetPasswordValidator:AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator()
        {

            RuleFor(x => x.Email)
                  .NotEmpty().WithMessage("Email is required.")
                  .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .Length(6).WithMessage("Code must be exactly 6 digits.");


        }
    }
}
