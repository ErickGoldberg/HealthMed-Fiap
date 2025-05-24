namespace HealthMed.Domain.Entities
{
    public class Doctor : EntityBase
    {
        public string Name { get; private set; }
        public string Crm { get; private set; }
        public string Specialty { get; private set; }

        private readonly List<DoctorAvailability> _availabilities = new();
        public IReadOnlyCollection<DoctorAvailability> Availabilities => _availabilities.AsReadOnly();

        public Doctor(Guid id, string name, string crm, string specialty)
        {
            Id = id;
            Name = name;
            Crm = crm;
            Specialty = specialty;
        }

        public void AddAvailability(DateTime startTime, DateTime endTime)
        {
            _availabilities.Add(new DoctorAvailability(startTime, endTime));
        }

        public void RemoveAvailability(Guid availabilityId)
        {
            _availabilities.RemoveAll(a => a.Id == availabilityId);
        }
    }
}