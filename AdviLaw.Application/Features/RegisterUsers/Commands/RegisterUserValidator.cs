using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Domain.Enums;
using FluentValidation;


namespace AdviLaw.Application.Features.RegisterUsers.Commands
{ 
   public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Dto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Dto.Role)
                .IsInEnum().WithMessage("Invalid role selected.");

            // Password (at least 6 characters, including upper, lower, number, and special character)  
            RuleFor(x => x.Dto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\@\!\?\*\.]").WithMessage("Password must contain at least one special character (@!?*.).");

            // Username  
            RuleFor(x => x.Dto.UserName)
                .NotEmpty().WithMessage("Username is required.");
                

            // Phone Number  
            //RuleFor(x => x.Dto.PhoneNumber)
            //    .NotEmpty().WithMessage("Phone number is required.")
            //    .Matches(@"^01[012]\d{8}$").WithMessage("Phone number must be a valid Egyptian number (e.g., 01012345678).");

            // Address Fields  
            RuleFor(x => x.Dto.City)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(x => x.Dto.Country)
                .NotEmpty().WithMessage("Country is required.");



            RuleFor(x => x.Dto.NationalityId)
                    .NotEmpty().WithMessage("National ID is required.")
                    .InclusiveBetween(10000000000000, 99999999999999).WithMessage("National ID must be exactly 14 digits.");


            // Lawyer-specific (if role is Lawyer)
            //When(x => x.Dto.Role == Roles.Lawyer, () =>
            //{
            //    RuleFor(x => x.Dto.ProfileHeader)
            //        .NotEmpty().WithMessage("Profile header is required.");

            //    RuleFor(x => x.Dto.ProfileAbout)
            //        .NotEmpty().WithMessage("Profile about is required.")
            //        .MaximumLength(1000).WithMessage("Profile about must not exceed 1000 characters.");

            //    RuleFor(x => x.Dto.LawyerCardID)
            //        .NotNull().WithMessage("Lawyer card ID is required for lawyers.")
            //        .GreaterThan(0).WithMessage("Lawyer card ID must be greater than 0.");

            //    RuleFor(x => x.Dto.Bio)
            //        .NotEmpty().WithMessage("Bio is required for lawyers.")
            //        .MaximumLength(500).WithMessage("Bio must not exceed 500 characters.");

            //    RuleFor(x => x.Dto.BarCardImagePath)
            //        .NotEmpty().WithMessage("Bar card image path is required for lawyers.");

            //    RuleFor(x => x.Dto.BarAssociationCardNumber)
            //        .NotNull().WithMessage("Bar association card number is required for lawyers.")
            //        .GreaterThan(0).WithMessage("Bar association card number must be greater than 0.");
            //});
        }
    }
}
