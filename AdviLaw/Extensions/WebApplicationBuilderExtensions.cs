using Microsoft.OpenApi.Models;

namespace AdviLaw.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {

            builder.Services.AddControllers();

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


        }
    }
}
