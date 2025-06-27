using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AdviLaw.Application.Features.Lawyers.Commands.CreateLawyer
{
    public class CreateLawyerCommandValidator: AbstractValidator<CreateLawyerCommand>
    {
        public CreateLawyerCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");
            RuleFor(x => x.BarCardImage)
                .NotEmpty().WithMessage("BarCardImage is required");
            RuleFor(x => x.NationalIDImage)
           .NotEmpty().WithMessage("NationalIDImage is required");
            //RuleFor(x => x.ProfileAbout)
            //    .NotEmpty().WithMessage("Profile about is required.")
            //    .MaximumLength(1000).WithMessage("Profile about cannot exceed 1000 characters.");

            //RuleFor(x => x.LawyerCardID)
            //    .GreaterThan(0).WithMessage("Lawyer card ID must be greater than zero.");

            //RuleFor(x => x.Bio)
            //    .NotEmpty().WithMessage("Bio is required.")
            //    .MaximumLength(1000).WithMessage("Bio cannot exceed 1000 characters.");

            //RuleFor(x => x.BarCardImagePath)
            //    .NotEmpty().WithMessage("Bar card image path is required.");

            //RuleFor(x => x.BarAssociationCardNumber)
            //    .GreaterThan(0).WithMessage("Bar association card number must be greater than zero.");
        }

    }
}
