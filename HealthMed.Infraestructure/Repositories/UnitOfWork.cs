using HealthMed.Domain.Repositories;
using HealthMed.Infrastructure.Persistence;

namespace HealthMed.Infrastructure.Repositories
{
    public class UnitOfWork(HealthMedDbContext context) : IUnitOfWork
    {
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
