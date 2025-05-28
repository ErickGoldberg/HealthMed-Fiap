using HealthMed.Domain.Entities;
using HealthMed.Domain.Repositories;
using HealthMed.Infrastructure.Persistence;

namespace HealthMed.Infrastructure.Repositories
{
    public class DoctorAvailabilityRepository : Repository<DoctorAvailability>, IDoctorAvailabilityRepository
    {
        public DoctorAvailabilityRepository(HealthMedDbContext context) : base(context) { }
    }
}
