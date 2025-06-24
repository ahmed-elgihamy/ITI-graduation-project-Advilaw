using FluentValidation;

namespace AdviLaw.Application.JobFields.Command.UpdateJobField
{
    public class UpdateJobFieldValidator : AbstractValidator<UpdateJobFieldCommand>
    {
        public UpdateJobFieldValidator()
        {

            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("Name is required")
                 .Length(3, 100);

            RuleFor(x => x.Description)
                .MaximumLength(300);
        }
    }
}
