namespace HealthMed.Application.InputModels
{
    public class AddAvailabilityInputModel
    {
        public Guid DoctorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}