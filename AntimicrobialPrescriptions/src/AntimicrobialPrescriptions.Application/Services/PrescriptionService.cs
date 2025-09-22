using AntimicrobialPrescriptions.Application.DTOs;
using AntimicrobialPrescriptions.Application.Interfaces;
using AntimicrobialPrescriptions.Domain.Entities;
using AntimicrobialPrescriptions.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntimicrobialPrescriptions.Application.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionService(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        public async Task<Guid> CreateAsync(PrescriptionDto dto)
        {
            var prescription = new Prescription(
                dto.PatientId,
                dto.AntimicrobialName,
                dto.Dose,
                dto.Frequency,
                dto.Route,
                dto.Indication,
                dto.StartDate,
                dto.ExpectedEndDate,
                dto.PrescriberName,
                dto.PrescriberRole);

            await _prescriptionRepository.AddAsync(prescription);
            return prescription.Id;
        }

        public async Task<IEnumerable<PrescriptionDto>> GetAllAsync()
        {
            var prescriptions = await _prescriptionRepository.GetAllAsync();
            return prescriptions.Select(ToDto);
        }

        public async Task<PrescriptionDto?> GetByIdAsync(Guid id)
        {
            var prescription = await _prescriptionRepository.GetByIdAsync(id);
            if (prescription == null)
                return null;     
            return ToDto(prescription);
        }

        public async Task<IEnumerable<PrescriptionDto>> GetByPatientIdAsync(string patientId)
        {
            var prescriptions = await _prescriptionRepository.GetByPatientIdAsync(patientId);
            return prescriptions.Select(ToDto);
        }

        public async Task MarkReviewedAsync(Guid id)
        {
            var prescription = await _prescriptionRepository.GetByIdAsync(id);
            if (prescription == null)
                throw new ArgumentException("Prescription not found");

            prescription.MarkReviewed();
            await _prescriptionRepository.UpdateAsync(prescription);
        }

        public async Task DiscontinueAsync(Guid id)
        {
            var prescription = await _prescriptionRepository.GetByIdAsync(id);
            if (prescription == null)
                throw new ArgumentException("Prescription not found");

            prescription.Discontinue();
            await _prescriptionRepository.UpdateAsync(prescription);
        }

        public async Task UpdateAsync(PrescriptionDto dto)
        {
            var prescription = await _prescriptionRepository.GetByIdAsync(dto.Id);
            if (prescription == null)
                throw new ArgumentException("Prescription not found");

          

            var updated = new Prescription(
                dto.PatientId,
                dto.AntimicrobialName,
                dto.Dose,
                dto.Frequency,
                dto.Route,
                dto.Indication,
                dto.StartDate,
                dto.ExpectedEndDate,
                dto.PrescriberName,
                dto.PrescriberRole);

            var idProperty = typeof(Prescription).GetProperty("Id");
            if (idProperty == null)
            {
                throw new InvalidOperationException("Property 'Id' not found on Prescription type.");
            }
            idProperty.SetValue(updated, dto.Id);

            var statusProperty = typeof(Prescription).GetProperty("Status");
            if (statusProperty == null)
            {
                throw new InvalidOperationException("Property 'Status' not found on Prescription type.");
            }
            statusProperty.SetValue(updated, dto.Status);

            await _prescriptionRepository.UpdateAsync(updated);
        }

        private static PrescriptionDto ToDto(Prescription p)
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
    }
}
