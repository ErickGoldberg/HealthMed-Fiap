using HealthMed.Application.Abstraction;
using HealthMed.Application.DTOs;
using HealthMed.Application.InputModels;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Entities.Enums;
using HealthMed.Domain.Repositories;

namespace HealthMed.Application.Services
{
    public class PatientService(
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork) : IPatientService
    {
        public async Task<Result<List<DoctorDto>>> GetAvailableDoctorsAsync(string? specialty)
        {
            var doctors = await doctorRepository.GetAvailableDoctorsAsync(specialty);

            var dtos = doctors.Select(d => new DoctorDto
            {
                Id = d.Id,
                Name = d.Name,
                Specialty = d.Specialty
            }).ToList();

            return Result<List<DoctorDto>>.Success(dtos);
        }

        public async Task<Result> BookAppointmentAsync(BookAppointmentInputModel input)
        {
            var appointment = new Appointment(input.DoctorId, input.PatientId, input.ScheduledAt, input.Price, input.Reason);

            await appointmentRepository.AddAsync(appointment);
            await unitOfWork.CommitAsync();

            return Result.Success("Consulta agendada com sucesso.");
        }

        public async Task<Result> CancelAppointmentAsync(Guid appointmentId, string reason)
        {
            var appointment = await appointmentRepository.GetByIdAsync(appointmentId);

            if (appointment == null)
                return Result.NotFound("Consulta não encontrada.");

            appointment.Status = AppointmentStatus.Canceled;
            appointment.CancellationReason = reason;
            await unitOfWork.CommitAsync();

            return Result.Success("Consulta cancelada com sucesso.");
        }

        public async Task<Result<List<PatientDto>>> GetAllPatientsAsync()
        {
            var patients = await patientRepository.GetAllAsync();

            var dtos = patients.Select(p => new PatientDto
            {
                Id = p.Id,
                Name = p.FullName,
                Email = p.Email,
                Phone = p.Phone
            }).ToList();

            return Result<List<PatientDto>>.Success(dtos);
        }

        public async Task<Result<PatientDto>> GetPatientByIdAsync(Guid id)
        {
            var patient = await patientRepository.GetByIdAsync(id);

            if (patient == null)
                return Result<PatientDto>.NotFound("Paciente não encontrado.");

            return Result<PatientDto>.Success(new PatientDto
            {
                Id = patient.Id,
                Name = patient.FullName,
                DateOfBirth = patient.DateOfBirth,
                Email = patient.Email,
                Phone = patient.Phone,
                Cpf = patient.Cpf
            });
        }

        public async Task<Result> CreatePatientAsync(CreateOrEditPatientInputModel input)
        {
            var patient = new Patient(input.Name, input.Email, input.Cpf, input.Phone, input.DateOfBirth);

            await patientRepository.AddAsync(patient);
            await unitOfWork.CommitAsync();

            return Result.Success("Paciente cadastrado com sucesso.");
        }

        public async Task<Result> UpdatePatientAsync(CreateOrEditPatientInputModel input)
        {
            var patient = await patientRepository.GetByIdAsync(input.Id);

            if (patient == null)
                return Result.NotFound("Paciente não encontrado.");

            if (!string.IsNullOrWhiteSpace(input.Name))
                patient.FullName = input.Name;

            if (!string.IsNullOrWhiteSpace(input.Phone))
                patient.Phone = input.Phone;

            if (!string.IsNullOrWhiteSpace(input.Cpf))
                patient.Cpf = input.Cpf;

            if (input.DateOfBirth != default)
                patient.DateOfBirth = input.DateOfBirth;

            await unitOfWork.CommitAsync();

            return Result.Success("Paciente atualizado com sucesso.");
        }

        public async Task<Result> DeletePatientAsync(Guid id)
        {
            var patient = await patientRepository.GetByIdAsync(id);

            if (patient == null)
                return Result.NotFound("Paciente não encontrado.");

            patientRepository.Remove(patient);
            await unitOfWork.CommitAsync();

            return Result.Success("Paciente deletado com sucesso.");
        }
    }
}