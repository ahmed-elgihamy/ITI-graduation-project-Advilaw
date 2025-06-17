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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using AdviLaw.Infrastructure.Repositories;

namespace AdviLaw.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddInfrastructure(this IServiceCollection services, IConfiguration conf)
        {
            var connection = conf.GetConnectionString("AdviLawDB");
            services.AddDbContext<AdviLawDBContext>(options => options.UseSqlServer(connection));
       
            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IPasswordResetCodeRepository, PasswordResetCodeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();


            services.AddIdentity<User, IdentityRole>()
                 .AddEntityFrameworkStores<AdviLawDBContext>()
                 .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
 {
     var jwtKey = conf["Jwt:Key"];
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = conf["Jwt:Issuer"],
         ValidAudience = conf["Jwt:Audience"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });


           
        }
    }
}
