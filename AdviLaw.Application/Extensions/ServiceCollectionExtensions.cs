<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Application.Behaviors;
using AdviLaw.Application.Specializations;
using AdviLaw.Domain.Repositories;
=======

using AdviLaw.Application.Basics;
using AdviLaw.Application.Behaviors;
>>>>>>> 7dc05f55d380fb9c71aaf4a7fa29b27d6f4b886c
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
<<<<<<< HEAD
using Microsoft.Win32;
=======
>>>>>>> 7dc05f55d380fb9c71aaf4a7fa29b27d6f4b886c

namespace AdviLaw.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddApplication(this IServiceCollection services)
        {
            var type = typeof(ServiceCollectionExtensions).Assembly;

            //Scan this assembly for any class that inherits from Profile (like CreateUserMappingProfile) and register its mapping rules.
            services.AddAutoMapper(type);
            //  services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(type));
            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .AddFluentValidationAutoValidation();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<ResponseHandler>();

<<<<<<< HEAD

           //register ResponseHandler in this AddApplication() method
           services.AddScoped<ResponseHandler>();


=======
            //register ResponseHandler in this AddApplication() method
            //services.AddScoped<ResponseHandler>();
>>>>>>> 7dc05f55d380fb9c71aaf4a7fa29b27d6f4b886c
        }
    }
}
