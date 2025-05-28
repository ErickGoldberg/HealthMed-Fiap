namespace HealthMed.Application.InputModels
{
    public class CreateOrEditPatientInputModel
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty; 
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}