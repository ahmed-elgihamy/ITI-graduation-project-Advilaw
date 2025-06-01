using AdviLaw.Application.Extensions;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Extensions;
using AdviLaw.Infrastructure.Extensions;
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

            app.MapGroup("api/identity")
                .WithTags("Identity")
                .MapIdentityApi<User>(); 
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
