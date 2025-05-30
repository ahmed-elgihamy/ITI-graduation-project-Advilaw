using AdviLaw.Application.Specializations.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Specializations.Command.CreateSpecialization
{
  public  class CreateSpecializationCommandValidator:AbstractValidator<CreateSpeciallizationCommand>
    {

        public CreateSpecializationCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 100)
                .WithMessage("enter Name between 3 or 100 char");
        }
    }
}
