using HealthMed.Application.Abstraction;
using HealthMed.Application.DTOs;
using HealthMed.Application.InputModels;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Entities.Enums;
using HealthMed.Domain.Repositories;

namespace HealthMed.Application.Services
{
    public class DoctorService(
        IDoctorRepository doctorRepository,
        IAppointmentRepository appointmentRepository,
        IDoctorAvailabilityRepository availabilityRepository,
        IUnitOfWork unitOfWork) : IDoctorService
    {
        public async Task<Result<DoctorDto>> GetDoctorByIdAsync(Guid id)
        {
            var doctor = await doctorRepository.GetByIdAsync(id);

            if (doctor == null)
                return Result<DoctorDto>.NotFound("Médico não encontrado.");

            return Result<DoctorDto>.Success(new DoctorDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialty = doctor.Specialty
            });
        }

        public async Task<Result> AddAvailabilityAsync(AddAvailabilityInputModel input)
        {
            var availability = new DoctorAvailability(input.DoctorId, input.StartTime, input.EndTime);

            await availabilityRepository.AddAsync(availability);
            await unitOfWork.CommitAsync();

            return Result.Success("Disponibilidade adicionada.");
        }

        public async Task<Result> RemoveAvailabilityAsync(Guid availabilityId)
        {
            var availability = await availabilityRepository.GetByIdAsync(availabilityId);

            if (availability == null)
                return Result.NotFound("Disponibilidade não encontrada.");

            availabilityRepository.Remove(availability);
            await unitOfWork.CommitAsync();

            return Result.Success("Disponibilidade removida.");
        }

        public async Task<Result> AcceptAppointmentAsync(Guid appointmentId)
        {
            var appointment = await appointmentRepository.GetByIdAsync(appointmentId);

            if (appointment == null)
                return Result.NotFound("Consulta não encontrada.");

            appointment.Status = AppointmentStatus.Accepted;
            await unitOfWork.CommitAsync();

            return Result.Success("Consulta aceita.");
        }

        public async Task<Result> RejectAppointmentAsync(Guid appointmentId)
        {
            var appointment = await appointmentRepository.GetByIdAsync(appointmentId);

            if (appointment == null)
                return Result.NotFound("Consulta não encontrada.");

            appointment.Status = AppointmentStatus.Rejected;
            await unitOfWork.CommitAsync();

            return Result.Success("Consulta recusada.");
        }

        public async Task<Result<List<DoctorDto>>> GetAllDoctorsAsync()
        {
            var doctors = await doctorRepository.GetAllAsync();

            var dtos = doctors.Select(d => new DoctorDto
            {
                Id = d.Id,
                Name = d.Name,
                Specialty = d.Specialty
            }).ToList();

            return Result<List<DoctorDto>>.Success(dtos);
        }

        public async Task<Result> CreateDoctorAsync(CreateOrEditDoctorInputModel input)
        {
            var doctor = new Doctor(input.Name, input.Crm, input.Specialty, input.Email, input.Phone);

            await doctorRepository.AddAsync(doctor);
            await unitOfWork.CommitAsync();

            return Result.Success("Médico cadastrado.");
        }

        public async Task<Result> UpdateDoctorAsync(CreateOrEditDoctorInputModel input)
        {
            var doctor = await doctorRepository.GetByIdAsync(input.Id);

            if (doctor == null)
                return Result.NotFound("Médico não encontrado.");

            if (!string.IsNullOrWhiteSpace(input.Name))
                doctor.Name = input.Name;

            if (!string.IsNullOrWhiteSpace(input.Specialty))
                doctor.Specialty = input.Specialty;

            if (!string.IsNullOrWhiteSpace(input.Phone))
                doctor.Phone = input.Phone;

            if (!string.IsNullOrWhiteSpace(input.Email))
                doctor.Email = input.Email;

            if (!string.IsNullOrWhiteSpace(input.Crm))
                doctor.Crm = input.Crm;

            await unitOfWork.CommitAsync();

            return Result.Success("Médico atualizado.");
        }

        public async Task<Result> DeleteDoctorAsync(Guid id)
        {
            var doctor = await doctorRepository.GetByIdAsync(id);

            if (doctor == null)
                return Result.NotFound("Médico não encontrado.");

            doctorRepository.Remove(doctor);
            await unitOfWork.CommitAsync();

            return Result.Success("Médico removido.");
        }
    }
}