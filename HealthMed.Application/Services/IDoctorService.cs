using HealthMed.Application.Abstraction;
using HealthMed.Application.DTOs;
using HealthMed.Application.InputModels;

namespace HealthMed.Application.Services
{
    public interface IDoctorService
    {
        Task<Result<DoctorDto>> GetDoctorByIdAsync(Guid id);
        Task<Result> AddAvailabilityAsync(AddAvailabilityInputModel input);
        Task<Result> RemoveAvailabilityAsync(Guid availabilityId);
        Task<Result> AcceptAppointmentAsync(Guid appointmentId);
        Task<Result> RejectAppointmentAsync(Guid appointmentId);

        Task<Result<List<DoctorDto>>> GetAllDoctorsAsync();
        Task<Result> CreateDoctorAsync(CreateOrEditDoctorInputModel input);
        Task<Result> UpdateDoctorAsync(CreateOrEditDoctorInputModel input);
        Task<Result> DeleteDoctorAsync(Guid id);
    }
}
