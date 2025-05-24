using HealthMed.API.Filters;
using Microsoft.OpenApi.Models;

namespace HealthMed.API.Extensions
{
    public static class ServiceConfiguration
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            //builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)))
            //    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<>());

            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HealthMed.API",
                    Version = "v1"
                });
            });

            return builder;
        }
    }
}