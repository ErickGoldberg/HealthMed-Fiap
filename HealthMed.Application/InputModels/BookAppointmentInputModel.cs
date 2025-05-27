namespace HealthMed.Application.InputModels
{
    public class BookAppointmentInputModel
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string Reason { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
