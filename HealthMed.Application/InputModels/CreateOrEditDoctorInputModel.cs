namespace HealthMed.Application.InputModels
{
    public class CreateOrEditDoctorInputModel
    {
        public Guid? Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
