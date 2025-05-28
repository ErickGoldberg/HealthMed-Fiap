namespace HealthMed.Domain.Entities
{
    public class Doctor : EntityBase
    {
        public string Name { get; set; }
        public string Crm { get; set; }
        public string Specialty { get; set; }
        public string Email { get; set; } 
        public string Phone { get; set; } 


        private readonly List<DoctorAvailability> _availabilities = new();
        public IReadOnlyCollection<DoctorAvailability> Availabilities => _availabilities.AsReadOnly();

        public Doctor(string name, string crm, string specialty, string email, string phone)
        {
            Name = name;
            Crm = crm;
            Specialty = specialty;
            Email = email;
            Phone = phone;
        }

        public void AddAvailability(DateTime startTime, DateTime endTime)
        {
            _availabilities.Add(new DoctorAvailability(Id, startTime, endTime));
        }

        public void RemoveAvailability(Guid availabilityId)
        {
            _availabilities.RemoveAll(a => a.Id == availabilityId);
        }
    }
}