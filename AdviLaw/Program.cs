using System.Text.Json.Serialization;
using AdviLaw.Application.Behaviors;
using AdviLaw.Application.Extensions;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AdviLaw.Extensions;
using AdviLaw.Infrastructure.Extensions;
using AdviLaw.Infrastructure.UnitOfWork;
using AdviLaw.MiddleWare;
using MediatR;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace AdviLaw
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); 
                });
            });

           
            builder.Services.AddAuthorization();
            builder.AddPresentation();                 
            builder.Services.AddApplication();           
            builder.Services.AddInfrastructure(builder.Configuration); 

            builder.Services.AddControllers()

                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });



            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {


                app.UseMiddleware<ErrorHandlerMiddleware>();


                app.UseSwagger();
                app.UseSwaggerUI();
            }

           
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });

           
            app.UseMiddleware<ErrorHandlerMiddleware>(); 
            app.UseHttpsRedirection();

          
            app.UseCors("AllowAngularApp");



            app.UseCors("AllowAll");

            app.UseAuthorization();
            app.UseCors();
            app.MapControllers();

            app.Run();
        }
    }
}
