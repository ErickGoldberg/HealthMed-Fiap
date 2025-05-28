using HealthMed.Domain.Entities;
using HealthMed.Domain.Repositories;
using HealthMed.Infrastructure.Persistence;

namespace HealthMed.Infrastructure.Repositories
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(HealthMedDbContext context) : base(context) { }
    }
}
