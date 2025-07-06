using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace AdviLaw.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {

            //builder.Services.AddControllers();

            builder.Services.AddControllers()
                .AddJsonOptions(x =>
                    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            builder.Services.AddSwaggerGen(
              c =>
              {
                  c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                  {
                      Type = SecuritySchemeType.Http,
                      Scheme = "bearer"
                  });
                  c.AddSecurityRequirement(new OpenApiSecurityRequirement
              {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference =new OpenApiReference{Type=ReferenceType.SecurityScheme,Id="bearerAuth"}
                    },
                    []
                }
               });
              });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDev", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:4200") 
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials(); 
                });
            });

            builder.Services.AddSignalR();
        }
    }
}
