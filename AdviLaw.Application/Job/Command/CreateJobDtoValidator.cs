//using AdviLaw.Application.Job.Dtos;
//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AdviLaw.Application.Job.Command
//{
   
//    public class CreateJobDtoValidator : AbstractValidator<CreateJobDto>
//    {
//        public CreateJobDtoValidator()
//        {
//            RuleFor(x => x.Header).NotEmpty().WithMessage("Job header is required");
//            RuleFor(x => x.Description).NotEmpty().WithMessage("Job description is required");
//            RuleFor(x => x.budget).GreaterThan(0).WithMessage("Budget must be greater than 0");
//            RuleFor(x => x.JobFieldName).NotEmpty().WithMessage("Job field is required");
//        }
//    }

//}
