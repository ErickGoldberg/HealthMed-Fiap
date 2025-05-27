namespace HealthMed.Domain.Entities
{
    public class DoctorAvailability : EntityBase
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public DoctorAvailability(Guid doctorId, DateTime startTime, DateTime endTime)
        {
            Id = doctorId;
            StartTime = startTime;
            EndTime = endTime;

            if (StartTime >= EndTime)
                throw new ArgumentException("Start time must be before end time.");
        }
    }
}