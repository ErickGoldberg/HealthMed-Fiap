using HealthMed.Domain.Repositories;
using HealthMed.Infrastructure.Persistence;
using HealthMed.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMed.Infrastructure.Extensions
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPersistence(configuration)
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<HealthMedDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        }
    }
}
