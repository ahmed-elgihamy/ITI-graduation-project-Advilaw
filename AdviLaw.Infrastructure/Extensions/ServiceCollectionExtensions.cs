using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.AppointmentSection.DTOs;
using AdviLaw.Application.Features.Shared.DTOs;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Domain.UnitOfWork;
using AdviLaw.Infrastructure.Persistence;
using AdviLaw.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace AdviLaw.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddInfrastructure(this IServiceCollection services, IConfiguration conf)
        {
            var connection = conf.GetConnectionString("AdviLawDB");
            services.AddDbContext<AdviLawDBContext>(options => options.UseSqlServer(connection));

            services.AddScoped<ITokenService, TokenService>();


            //services.AddIdentityApiEndpoints<User>()
            //    .AddEntityFrameworkStores<AdviLawDBContext>();
            services.AddIdentity<User, IdentityRole>()
                 .AddEntityFrameworkStores<AdviLawDBContext>()
                 .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
            {
              
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";

               
            });

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
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                     RoleClaimType = ClaimTypes.Role 
                 };
             });


            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPasswordResetCodeRepository, PasswordResetCodeRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            // In Program.cs or startup
            //services.AddScoped<IRequestHandler<GetPendingConsultationsQuery, Response<PagedResponse<AppointmentDetailsDTO>>>, GetPendingConsultationsHandler>();
            //;


            services.AddHttpContextAccessor();
           // services.AddScoped<IRequestHandler<GetPendingConsultationsQuery, Response<PagedResponse<AppointmentDetailsDTO>>>, GetPendingConsultationsHandler>();
            services.AddScoped<ILawyerRepository, LawyerRepository>();

        }
    }
}
