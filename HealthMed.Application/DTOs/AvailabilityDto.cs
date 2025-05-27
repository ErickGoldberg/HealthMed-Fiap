namespace HealthMed.Application.DTOs
{
    public class AvailabilityDto
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}