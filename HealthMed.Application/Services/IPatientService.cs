using HealthMed.Application.Abstraction;
using HealthMed.Application.DTOs;
using HealthMed.Application.InputModels;

namespace HealthMed.Application.Services
{
    public interface IPatientService
    {
        Task<Result<List<DoctorDto>>> GetAvailableDoctorsAsync(string? specialty);
        Task<Result> BookAppointmentAsync(BookAppointmentInputModel input);
        Task<Result> CancelAppointmentAsync(Guid appointmentId, string reason);

        Task<Result<List<PatientDto>>> GetAllPatientsAsync();
        Task<Result<PatientDto>> GetPatientByIdAsync(Guid id);
        Task<Result> CreatePatientAsync(CreateOrEditPatientInputModel input);
        Task<Result> UpdatePatientAsync(CreateOrEditPatientInputModel input);
        Task<Result> DeletePatientAsync(Guid id);
    }
}