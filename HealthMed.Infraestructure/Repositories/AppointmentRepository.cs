using HealthMed.Domain.Entities;
using HealthMed.Domain.Repositories;
using HealthMed.Infrastructure.Persistence;

namespace HealthMed.Infrastructure.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(HealthMedDbContext context) : base(context) { }
    }
}
