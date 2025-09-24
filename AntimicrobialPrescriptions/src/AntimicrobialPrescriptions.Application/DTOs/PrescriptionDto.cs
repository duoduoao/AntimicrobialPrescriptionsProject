using AntimicrobialPrescriptions.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntimicrobialPrescriptions.Application.DTOs
{
    public class PrescriptionDto
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
        public PrescriptionStatus Status { get; set; }
    }
}
