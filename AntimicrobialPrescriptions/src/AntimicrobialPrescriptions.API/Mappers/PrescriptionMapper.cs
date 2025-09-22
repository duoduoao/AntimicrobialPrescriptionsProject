using AntimicrobialPrescriptions.API.Models;
using AntimicrobialPrescriptions.Domain.Enums;

namespace AntimicrobialPrescriptions.API.Mappings
{
    public static class PrescriptionMapper
    {
        public static ApiPrescriptionStatus MapToApiStatus(Domain.Enums.PrescriptionStatus domainStatus) =>
            domainStatus switch
            {
                Domain.Enums.PrescriptionStatus.Active => ApiPrescriptionStatus.Active,
                Domain.Enums.PrescriptionStatus.Reviewed => ApiPrescriptionStatus.Reviewed,
                Domain.Enums.PrescriptionStatus.Discontinued => ApiPrescriptionStatus.Discontinued,
                _ => throw new ArgumentOutOfRangeException(nameof(domainStatus))
            };

        public static Domain.Enums.PrescriptionStatus MapToDomainStatus(ApiPrescriptionStatus apiStatus) =>
            apiStatus switch
            {
                ApiPrescriptionStatus.Active => Domain.Enums.PrescriptionStatus.Active,
                ApiPrescriptionStatus.Reviewed => Domain.Enums.PrescriptionStatus.Reviewed,
                ApiPrescriptionStatus.Discontinued => Domain.Enums.PrescriptionStatus.Discontinued,
                _ => throw new ArgumentOutOfRangeException(nameof(apiStatus))
            };
    }
}
