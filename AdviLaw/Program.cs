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

            //serialize and deserialize enums as strings instead of integers.
            builder.Services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                      options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                     });



            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
<<<<<<< HEAD
=======

>>>>>>> 7dc05f55d380fb9c71aaf4a7fa29b27d6f4b886c
            app.UseMiddleware<ErrorHandlerMiddleware>();

                app.UseSwagger();
                app.UseSwaggerUI();
<<<<<<< HEAD
            }

            app.MapGroup("api/identity")
                .WithTags("Identity")
                .MapIdentityApi<User>(); 
=======

            }


            // Redirect root URL to Swagger UI
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });




>>>>>>> 7dc05f55d380fb9c71aaf4a7fa29b27d6f4b886c

            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
