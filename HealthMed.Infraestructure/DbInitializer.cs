using HealthMed.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMed.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<DbContextOptions<HealthMedDbContext>>();

            using var context = new HealthMedDbContext(options);
            context.Database.Migrate();
        }
    }
}
