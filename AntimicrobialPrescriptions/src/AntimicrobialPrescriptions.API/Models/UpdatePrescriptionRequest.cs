using System.ComponentModel.DataAnnotations;

namespace AntimicrobialPrescriptions.API.Models
{
    public class UpdatePrescriptionRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string PatientId { get; set; } = string.Empty;

        [Required]
        public string AntimicrobialName { get; set; } = string.Empty;

        [Required]
        public string Dose { get; set; } = string.Empty;

        [Required]
        public string Frequency { get; set; } = string.Empty;

        [Required]
        public string Route { get; set; } = string.Empty;

        [Required]
        public string Indication { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime ExpectedEndDate { get; set; }

        [Required]
        public string PrescriberName { get; set; } = string.Empty;

        [Required]
        public string PrescriberRole { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }

}
