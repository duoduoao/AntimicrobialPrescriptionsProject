using AntimicrobialPrescriptions.Application.DTOs;
using AntimicrobialPrescriptions.Application.Interfaces;
using AntimicrobialPrescriptions.Application.Services;
using AntimicrobialPrescriptions.Domain.Entities;
using AntimicrobialPrescriptions.Domain.Enums;
using AntimicrobialPrescriptions.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AntimicrobialPrescriptions.Application.Tests.Services
{
    public class PrescriptionServiceTests
    {
        private readonly Mock<IPrescriptionRepository> _repositoryMock;
        private readonly IPrescriptionService _service;

        public PrescriptionServiceTests()
        {
            _repositoryMock = new Mock<IPrescriptionRepository>();
            _service = new PrescriptionService(_repositoryMock.Object);
        }

        private Prescription CreateDomainEntity(Guid? id = null, PrescriptionStatus status = PrescriptionStatus.Active)
        {
            var prescription = new Prescription(
                patientId: "PAT001",
                antimicrobialName: "Amoxicillin",
                dose: "500mg",
                frequency: "3 times a day",
                route: "Oral",
                indication: "Infection",
                startDate: DateTime.UtcNow.Date,
                expectedEndDate: DateTime.UtcNow.Date.AddDays(7),
                prescriberName: "Dr. Who",
                prescriberRole: "Clinician"
            );

            if (id.HasValue)
            {
                var idProperty = typeof(Prescription).GetProperty("Id");
                if (idProperty == null)
                {
                    throw new InvalidOperationException("Property 'Id' not found on Prescription type.");
                }
                idProperty.SetValue(prescription, id.Value);

                var statusProperty = typeof(Prescription).GetProperty("Status");
                if (statusProperty == null)
                {
                    throw new InvalidOperationException("Property 'Status' not found on Prescription type.");
                }
                statusProperty.SetValue(prescription, status);
            }


            return prescription;
        }

        private PrescriptionDto ToDto(Prescription p)
        {
            return new PrescriptionDto
            {
                Id = p.Id,
                PatientId = p.PatientId,
                AntimicrobialName = p.AntimicrobialName,
                Dose = p.Dose,
                Frequency = p.Frequency,
                Route = p.Route,
                Indication = p.Indication,
                StartDate = p.StartDate,
                ExpectedEndDate = p.ExpectedEndDate,
                PrescriberName = p.PrescriberName,
                PrescriberRole = p.PrescriberRole,
                Status = p.Status
            };
        }

        [Fact]
        public async Task CreateAsync_Should_AddPrescription_AndReturnId()
        {
            Prescription? savedPrescription = null;

            _repositoryMock.Setup(x => x.AddAsync(It.IsAny<Prescription>()))
                .Callback<Prescription>(p => savedPrescription = p)
                .Returns(Task.CompletedTask);

            var dto = new PrescriptionDto
            {
                PatientId = "PAT001",
                AntimicrobialName = "Amoxicillin",
                Dose = "500mg",
                Frequency = "3/day",
                Route = "Oral",
                Indication = "Infection",
                StartDate = DateTime.UtcNow.Date,
                ExpectedEndDate = DateTime.UtcNow.Date.AddDays(5),
                PrescriberName = "Dr. Who",
                PrescriberRole = "Clinician"
            };

            var newId = await _service.CreateAsync(dto);

            Assert.NotEqual(Guid.Empty, newId);
            Assert.NotNull(savedPrescription);
            Assert.Equal(dto.PatientId, savedPrescription.PatientId);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnDto_WhenFound()
        {
            var id = Guid.NewGuid();
            var prescription = CreateDomainEntity(id);
            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(prescription);

            var result = await _service.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(prescription.PatientId, result.PatientId);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Prescription)null);

            // Act
            var result = await _service.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);   
        }

        [Fact]
        public async Task MarkReviewedAsync_Should_UpdatePrescriptionStatus()
        {
            var id = Guid.NewGuid();
            var prescription = CreateDomainEntity(id);

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(prescription);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Prescription>())).Returns(Task.CompletedTask);

            await _service.MarkReviewedAsync(id);

            Assert.Equal(PrescriptionStatus.Reviewed, prescription.Status);

            _repositoryMock.Verify(r => r.UpdateAsync(prescription), Times.Once());
        }

        [Fact]
        public async Task DiscontinueAsync_Should_UpdatePrescriptionStatus()
        {
            var id = Guid.NewGuid();
            var prescription = CreateDomainEntity(id);

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(prescription);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Prescription>())).Returns(Task.CompletedTask);

            await _service.DiscontinueAsync(id);

            Assert.Equal(PrescriptionStatus.Discontinued, prescription.Status);

            _repositoryMock.Verify(r => r.UpdateAsync(prescription), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_Should_CallUpdate_WhenPrescriptionExists()
        {
            var id = Guid.NewGuid();
            var prescription = CreateDomainEntity(id);
            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(prescription);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Prescription>())).Returns(Task.CompletedTask);

            var dto = ToDto(prescription);

            await _service.UpdateAsync(dto);

            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Prescription>()), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_WhenNotFound()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Prescription)null);

            var dto = new PrescriptionDto { Id = Guid.NewGuid() };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.UpdateAsync(dto));
        }
    }
}
