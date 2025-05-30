using AdviLaw.Application.Extensions;
using AdviLaw.Infrastructure.Extensions;

namespace AdviLaw
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
      
      
            builder.Services.AddAuthorization();
            builder.Services.AddApplication();
            
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

           

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
