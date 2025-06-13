using AdviLaw.Application.Extensions;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AdviLaw.Extensions;
using AdviLaw.Infrastructure.Extensions;
using AdviLaw.Infrastructure.UnitOfWork;
using AdviLaw.MiddleWare;

namespace AdviLaw
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

    
            builder.Services.AddAuthorization();
            builder.AddPresentation();
            builder.Services.AddApplication();            
            builder.Services.AddInfrastructure(builder.Configuration);
           

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

            //app.MapGroup("api/identity")
            //    .WithTags("Identity")
            //    .MapIdentityApi<User>(); 

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
