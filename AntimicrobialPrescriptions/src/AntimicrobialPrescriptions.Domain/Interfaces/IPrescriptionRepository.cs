using AntimicrobialPrescriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntimicrobialPrescriptions.Domain.Interfaces
{
    public interface IPrescriptionRepository
    {
        Task<Prescription> GetByIdAsync(Guid id);
        Task<IEnumerable<Prescription>> GetByPatientIdAsync(string patientId);
        Task<IEnumerable<Prescription>> GetAllAsync();
        Task AddAsync(Prescription prescription);
        Task UpdateAsync(Prescription prescription);
        Task DeleteAsync(Guid id);
    }
}
