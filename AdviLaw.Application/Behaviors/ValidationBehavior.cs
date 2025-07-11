using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Behaviors
{


    //It's a MediatR pipeline behavior that runs before your command or
    //query handler. It checks if the request has validation rules and runs them.



    //////It implements IPipelineBehavior, which lets you 
    ///intercept requests before they reach the actual handler./////////
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        //this to see the validarion on the request
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {  //take the request and check if there is any validation on it or no
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    var message = failures.Select(x => x.PropertyName + ": " + x.ErrorMessage).FirstOrDefault();

                  //  throw new ValidationException(message);

                }
            }
            return await next();
        }
    }
}
