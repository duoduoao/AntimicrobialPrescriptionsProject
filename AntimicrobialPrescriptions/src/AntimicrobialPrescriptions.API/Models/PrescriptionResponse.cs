namespace AntimicrobialPrescriptions.API.Models
{
    public class PrescriptionResponse
    {
        public Guid Id { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string AntimicrobialName { get; set; } = string.Empty;
        public string Dose { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public string Indication { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public string PrescriberName { get; set; } = string.Empty;
        public string PrescriberRole { get; set; } = string.Empty;
        public ApiPrescriptionStatus Status { get; set; }
    }
   
}
