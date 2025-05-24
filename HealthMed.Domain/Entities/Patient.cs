namespace HealthMed.Domain.Entities
{
    public class Patient : EntityBase
    {
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public Patient(Guid id, string fullName, string email, string cpf)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Cpf = cpf;
        }
    }
}