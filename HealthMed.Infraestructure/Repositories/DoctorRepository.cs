using HealthMed.Domain.Entities;
using HealthMed.Domain.Repositories;
using HealthMed.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infrastructure.Repositories
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HealthMedDbContext context) : base(context) { }
        public async Task<List<Doctor>> GetAvailableDoctorsAsync(string? specialty)
        {
            var query = _context.Doctors.Include(d => d.Availabilities).AsQueryable();

            if (!string.IsNullOrWhiteSpace(specialty))
                query = query.Where(d => d.Specialty == specialty);

            return await query.ToListAsync();
        }
    }
}
