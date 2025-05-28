using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Repositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<List<Doctor>> GetAvailableDoctorsAsync(string? specialty);
    }
}
