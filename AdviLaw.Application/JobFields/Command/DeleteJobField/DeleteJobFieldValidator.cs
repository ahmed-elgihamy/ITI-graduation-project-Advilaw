using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.JobFields.Command.DeleteJobField
{
  public class DeleteJobFieldValidator : AbstractValidator<DeleteJobFieldCommand>
    {
        public DeleteJobFieldValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id must be greater than zero.");
        }
    }
}
