namespace HealthMed.Application.DTOs
{
    public class DoctorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
        public List<AvailabilityDto> Availabilities { get; set; } = new();
    }
}