using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.JobFields.Command.CreateJobField
{
   public class CreateJobFieldValidator : AbstractValidator<CreateJobFieldCommand>
    {
        public CreateJobFieldValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(3, 100);

            RuleFor(x => x.Description)
                .MaximumLength(300);
        }
    }
}
