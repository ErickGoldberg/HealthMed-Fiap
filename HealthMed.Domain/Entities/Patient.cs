namespace HealthMed.Domain.Entities
{
    public class Patient : EntityBase
    {
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Patient(string fullName, string email, string cpf, string phone, DateTime dateOfBirth)
        {
            FullName = fullName;
            Email = email;
            Cpf = cpf;
            Phone = phone;
            DateOfBirth = dateOfBirth;
        }
    }
}