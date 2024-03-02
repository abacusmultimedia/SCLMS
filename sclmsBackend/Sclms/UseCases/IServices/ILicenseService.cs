using Sclms.DTOS; 

namespace Sclms.UseCases.IServices;

public interface ILicenseService
{
    Task<LicenseAddUpdateResponseDTO> AddLicenseAsync(LicenseDTO license);
    Task<LicenseActivationResponseDTO> LicenseActivationAsync(LicenseActivateionDTO license);
    Task<LicenseDTO> GetLicenseByIdAsync(long licenseId);
    Task UpdateLicenseAsync(LicenseDTO license);
    Task DeleteLicenseAsync(long licenseId);
    Task<List<LicenseResponseDTO>> GetLicensesForAdmin(string userid);
    Task<List<LicenseResponseDTO>> GetLicensesForUser(string userid);
}

