using AdviLaw.Application.Specializations;
using AdviLaw.Domain.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddApplication(this IServiceCollection services)
        {
            var type = typeof(ServiceCollectionExtensions).Assembly;


          
            services.AddAutoMapper(type);
            //  services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(type));
            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .AddFluentValidationAutoValidation();



        }
    }
}
