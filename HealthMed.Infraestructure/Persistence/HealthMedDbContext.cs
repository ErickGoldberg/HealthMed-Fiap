using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infrastructure.Persistence
{
    public class HealthMedDbContext(DbContextOptions<HealthMedDbContext> options) : DbContext(options)
    {
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<DoctorAvailability> DoctorAvailabilities => Set<DoctorAvailability>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HealthMedDbContext).Assembly);
        }
    }
}
