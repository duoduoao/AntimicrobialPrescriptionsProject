using AntimicrobialPrescriptions.Domain.Enums;
using AntimicrobialPrescriptions.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntimicrobialPrescriptions.Domain.Entities
{
    public class Prescription
    {
        public Guid Id { get; private set; }
        public string PatientId { get; private set; }
        public string AntimicrobialName { get; private set; }
        public string Dose { get; private set; }
        public string Frequency { get; private set; }
        public string Route { get; private set; }
        public string Indication { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime ExpectedEndDate { get; private set; }
        public string PrescriberName { get; private set; }
        public string PrescriberRole { get; private set; }
        public PrescriptionStatus Status { get; private set; }

        
        public Prescription(
            string patientId,
            string antimicrobialName,
            string dose,
            string frequency,
            string route,
            string indication,
            DateTime startDate,
            DateTime expectedEndDate,
            string prescriberName,
            string prescriberRole)
        {
            Id = Guid.NewGuid();
            PatientId = patientId ?? throw new ArgumentNullException(nameof(patientId));
            AntimicrobialName = antimicrobialName ?? throw new ArgumentNullException(nameof(antimicrobialName));
            Dose = dose ?? throw new ArgumentNullException(nameof(dose));
            Frequency = frequency ?? throw new ArgumentNullException(nameof(frequency));
            Route = route ?? throw new ArgumentNullException(nameof(route));
            Indication = indication ?? throw new ArgumentNullException(nameof(indication));
            StartDate = startDate;
            ExpectedEndDate = expectedEndDate;
            PrescriberName = prescriberName ?? throw new ArgumentNullException(nameof(prescriberName));
            PrescriberRole = prescriberRole ?? throw new ArgumentNullException(nameof(prescriberRole));
            Status = PrescriptionStatus.Active;
        }

       
        public void MarkReviewed()
        {
            if (Status != PrescriptionStatus.Active)
            {
                throw new InvalidPrescriptionStatusException($"Cannot mark prescription as reviewed from status {Status}");
            }
            Status = PrescriptionStatus.Reviewed;
        }

        public void Discontinue()
        {
            if (Status == PrescriptionStatus.Discontinued)
            {
                throw new InvalidPrescriptionStatusException("Prescription already discontinued");
            }
            Status = PrescriptionStatus.Discontinued;
        }

       
    }
}
