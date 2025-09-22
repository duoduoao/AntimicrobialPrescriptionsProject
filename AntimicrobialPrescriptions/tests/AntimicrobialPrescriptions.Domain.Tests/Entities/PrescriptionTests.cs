using AntimicrobialPrescriptions.Domain.Entities;
using AntimicrobialPrescriptions.Domain.Enums;
using AntimicrobialPrescriptions.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AntimicrobialPrescriptions.Domain.Tests.Entities
{
    public class PrescriptionTests
    {

        private Prescription CreateDefaultPrescription()
        {
            return new Prescription(
                patientId: "PAT123",
                antimicrobialName: "Amoxicillin",
                dose: "500mg",
                frequency: "3 times a day",
                route: "Oral",
                indication: "Pneumonia",
                startDate: DateTime.Today,
                expectedEndDate: DateTime.Today.AddDays(7),
                prescriberName: "Dr. A",
                prescriberRole: "Clinician"
            );
        }

        [Fact]
        public void Constructor_ShouldInitializeWithActiveStatus()
        {
            var prescription = CreateDefaultPrescription();

            Assert.Equal(PrescriptionStatus.Active, prescription.Status);
            Assert.NotEqual(Guid.Empty, prescription.Id);
        }

        [Fact]
        public void MarkReviewed_FromActive_ShouldSetStatusToReviewed()
        {
            var prescription = CreateDefaultPrescription();

            prescription.MarkReviewed();

            Assert.Equal(PrescriptionStatus.Reviewed, prescription.Status);
        }

        [Fact]
        public void MarkReviewed_WhenNotActive_ThrowsException()
        {
            var prescription = CreateDefaultPrescription();

             
            prescription.MarkReviewed();

             
            var ex = Assert.Throws<InvalidPrescriptionStatusException>(() => prescription.MarkReviewed());

            Assert.Contains("Cannot mark prescription as reviewed", ex.Message);
        }

        [Fact]
        public void Discontinue_ShouldSetStatusToDiscontinued()
        {
            var prescription = CreateDefaultPrescription();

            prescription.Discontinue();

            Assert.Equal(PrescriptionStatus.Discontinued, prescription.Status);
        }

        [Fact]
        public void Discontinue_WhenAlreadyDiscontinued_ThrowsException()
        {
            var prescription = CreateDefaultPrescription();

            prescription.Discontinue();

            var ex = Assert.Throws<InvalidPrescriptionStatusException>(() => prescription.Discontinue());

            Assert.Equal("Prescription already discontinued", ex.Message);
        }

        [Fact]
        public void Constructor_WithNullPatientId_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Prescription(
                    patientId: null,
                    antimicrobialName: "Amoxicillin",
                    dose: "500mg",
                    frequency: "3 times a day",
                    route: "Oral",
                    indication: "Pneumonia",
                    startDate: DateTime.Today,
                    expectedEndDate: DateTime.Today.AddDays(7),
                    prescriberName: "Dr. A",
                    prescriberRole: "Clinician"
                ));
        }
    }
}
