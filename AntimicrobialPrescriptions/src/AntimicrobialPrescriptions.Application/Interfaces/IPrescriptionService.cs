using AntimicrobialPrescriptions.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntimicrobialPrescriptions.Application.Interfaces
{
    public interface IPrescriptionService
    {
        Task<PrescriptionDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<PrescriptionDto>> GetByPatientIdAsync(string patientId);
        Task<IEnumerable<PrescriptionDto>> GetAllAsync();
        Task<Guid> CreateAsync(PrescriptionDto newPrescription);
        Task UpdateAsync(PrescriptionDto updatedPrescription);
        Task MarkReviewedAsync(Guid id);
        Task DiscontinueAsync(Guid id);
    }
}
