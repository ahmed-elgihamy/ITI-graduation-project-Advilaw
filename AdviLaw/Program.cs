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
using Microsoft.Extensions.FileProviders;
using Stripe;
using System.Text.Json.Serialization;

namespace AdviLaw
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ Add CORS Policy
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

            // ✅ Configure controllers to serialize enums as strings
            builder.Services.AddControllers()
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

            // Redirect root URL to Swagger UI
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });

            app.UseHttpsRedirection();

            // ✅ UseRouting MUST be before CORS and Authorization
            app.UseRouting();

            // ✅ Apply CORS before Authorization
            app.UseCors("AllowAngularApp");

            app.UseAuthorization();

            // ✅ Stripe configuration
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

            // ✅ Static files from wwwroot
            app.UseStaticFiles();

            // ✅ Static files from "Uploads" folder
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
                RequestPath = "/Uploads"
            });

            app.MapControllers();

            app.Run();
        }
    }
}
