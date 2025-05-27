using HealthMed.Application.Abstraction;
using HealthMed.Application.DTOs;
using HealthMed.Application.InputModels;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Entities.Enums;

namespace HealthMed.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;

        public PatientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<DoctorDto>>> GetAvailableDoctorsAsync(string? specialty)
        {
            var query = _context.Doctors.Include(d => d.Availabilities).AsQueryable();

            if (!string.IsNullOrWhiteSpace(specialty))
                query = query.Where(d => d.Specialty == specialty);

            var doctors = await query.ToListAsync();

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

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return Result.Success("Consulta agendada com sucesso.");
        }

        public async Task<Result> CancelAppointmentAsync(Guid appointmentId, string reason)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);

            if (appointment == null)
                return Result.NotFound("Consulta não encontrada.");

            appointment.Status = AppointmentStatus.Canceled;
            appointment.CancellationReason = reason;
            await _context.SaveChangesAsync();

            return Result.Success("Consulta cancelada com sucesso.");
        }

        public async Task<Result<List<PatientDto>>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients.ToListAsync();

            var dtos = patients.Select(p => new PatientDto
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone
            }).ToList();

            return Result<List<PatientDto>>.Success(dtos);
        }

        public async Task<Result<PatientDto>> GetPatientByIdAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
                return Result<PatientDto>.NotFound("Paciente não encontrado.");

            return Result<PatientDto>.Success(new PatientDto
            {
                Id = patient.Id,
                Name = patient.Name,
                DateOfBirth = patient.DateOfBirth,
                Email = patient.Email,
                Phone = patient.Phone,
                Cpf = patient.Cpf
            });
        }

        public async Task<Result> CreatePatientAsync(CreateOrEditPatientInputModel input)
        {
            var patient = new Patient(input.Name, input.Email, input.Cpf, input.Phone, input.DateOfBirth);

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return Result.Success("Paciente cadastrado com sucesso.");
        }

        public async Task<Result> UpdatePatientAsync(CreateOrEditPatientInputModel input)
        {
            var patient = await _context.Patients.FindAsync(input.Id);

            if (patient == null)
                return Result.NotFound("Paciente não encontrado.");

            if(!string.IsNullOrWhiteSpace(input.Name))
                patient.Name = input.Name;

            if (!string.IsNullOrWhiteSpace(input.Phone))
                patient.Phone = input.Phone;

            if (!string.IsNullOrWhiteSpace(input.Cpf))
                patient.Cpf = input.Cpf;

            if (input.DateOfBirth != default)
                patient.DateOfBirth = input.DateOfBirth;

            await _context.SaveChangesAsync();

            return Result.Success("Paciente atualizado com sucesso.");
        }

        public async Task<Result> DeletePatientAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
                return Result.NotFound("Paciente não encontrado.");

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return Result.Success("Paciente deletado com sucesso.");
        }
    }
}
