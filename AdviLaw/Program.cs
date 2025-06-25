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

namespace AdviLaw
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();
            builder.AddPresentation();
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            var app = builder.Build();

            // ✅ Always use custom error middleware
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection(); // optional if SSL enabled
            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });

            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhandled Exception: " + ex.Message);
                throw;
            }


            //    var builder = WebApplication.CreateBuilder(args);


            //    builder.Services.AddAuthorization();
            //    builder.AddPresentation();
            //    builder.Services.AddApplication();
            //    builder.Services.AddInfrastructure(builder.Configuration);

            //    //serialize and deserialize enums as strings instead of integers.
            //    builder.Services.AddControllers()
            //            .AddJsonOptions(options =>
            //            {
            //              options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            //             });



            //    var app = builder.Build();

            //    if (app.Environment.IsDevelopment())
            //    {

            //    app.UseMiddleware<ErrorHandlerMiddleware>();


            //    }

            //    app.UseSwagger();
            //    app.UseSwaggerUI();

            //    // Redirect root URL to Swagger UI
            //    app.MapGet("/", context =>
            //    {
            //        context.Response.Redirect("/swagger");
            //        return Task.CompletedTask;
            //    });





            //    app.UseAuthorization();

            //    app.MapControllers();
            //    app.Run();
        }
    }
}
