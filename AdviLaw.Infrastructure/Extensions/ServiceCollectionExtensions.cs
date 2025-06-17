using System.Text;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Domain.UnitOfWork;
using AdviLaw.Infrastructure.Persistence;
using AdviLaw.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
<<<<<<< HEAD
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

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
=======
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


           
>>>>>>> a9cd0256916f72935696c6587dc9f3f5a55de04d
        }
    }
}
