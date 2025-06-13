using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Domain.Entities;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Domain.UnitOfWork;
using AdviLaw.Infrastructure.Persistence;
using AdviLaw.Infrastructure.Repositories;
using AdviLaw.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdviLaw.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddInfrastructure(this IServiceCollection services, IConfiguration conf)
        {
            var connection = conf.GetConnectionString("AdviLawDB");
            services.AddDbContext<AdviLawDBContext>(options => options.UseSqlServer(connection));
            services.AddScoped<ISpecializationRepository, SpecializationRepository>();

            //services.AddIdentityApiEndpoints<User>()
            //    .AddEntityFrameworkStores<AdviLawDBContext>();

            services.AddIdentity<User, IdentityRole>(options =>
            {
                //  Password rules..
                options.Password.RequireDigit = true;
            })
                  .AddEntityFrameworkStores<AdviLawDBContext>()
                  .AddDefaultTokenProviders();

             

            services.AddScoped<IUnitOfWork, AdviLaw.Infrastructure.UnitOfWork.UnitOfWork>();
        }
    }
}
