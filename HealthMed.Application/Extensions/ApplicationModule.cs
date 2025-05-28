using HealthMed.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMed.Application.Extensions
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddServices();

            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDoctorService, DoctorService>();
        }
    }
}
