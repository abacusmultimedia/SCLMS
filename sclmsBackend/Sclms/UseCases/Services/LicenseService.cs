
using Microsoft.EntityFrameworkCore;
using Sclms.DTOS;
using Sclms.Persistence.Modles;
using Sclms.UseCases.IServices;

public class LicenseService : ILicenseService
{
    private readonly IRepository<License> _licenseRepository;
    private readonly IRepository<AppUsers> _userRepository;

    private readonly IEmailSender _emailSender;
    public LicenseService(IRepository<License> licenseRepository, IRepository<AppUsers> userRepository
         , IEmailSender emailSender
        )
    {
        _licenseRepository = licenseRepository;
        _userRepository = userRepository;
        _emailSender = emailSender;
    }

    public async Task<LicenseAddUpdateResponseDTO> AddLicenseAsync(LicenseDTO license)
    {
        var response = new LicenseAddUpdateResponseDTO { IsSuccessful = false, Message = "" };
        // check if user exists 
        var user = _userRepository.GetAllAsync().FirstOrDefault(x => x.NormalizedUserName == license.UserName.ToUpper());
        if (user == null)
        {
            response.Message = "User Not found";
        }
        else
        {
            Guid newGuid = Guid.NewGuid();
            var licnce = _licenseRepository.GetAllAsync().FirstOrDefault(x => x.ProductId == license.ProductId && x.UserId == user.Id);
            if (licnce == null)
            {
                await _licenseRepository.AddAsync(new License { Key = newGuid.ToString(), ProductId = license.ProductId, UserId = user.Id }); // to be updated
                response.IsSuccessful = true; response.Message = "License added Successfuly";
            }
            else
            {
                licnce.Key = newGuid.ToString();
                await _licenseRepository.UpdateAsync(licnce);
                response.IsSuccessful = true; response.Message = "License Updated Successfuly";
            }

            //_emailSender.SendEmail("code.shakir@gmail.com", user.Email, "License key Updated Successfuly", $"Dear User please note that you product license key has been updated, you new key is {newGuid}");
        }
        return response;
    }


    public async Task<LicenseActivationResponseDTO> LicenseActivationAsync(LicenseActivateionDTO license)
    {
        var response = new LicenseActivationResponseDTO { IsSuccessful = false, Message = "" };
        // check if user exists 
        var user = _userRepository.GetAllAsync().FirstOrDefault(x => x.NormalizedUserName == license.UserName.ToUpper());
        if (user == null)
        {
            response.Message = "User Not found";
        }
        else
        {
            var IsValidProductExists = _licenseRepository.GetAllAsync().FirstOrDefault(x => x.ProductId == license.ProductId && x.Key == license.Key && x.UserId == user.Id);
            if (IsValidProductExists != null)
            {
                IsValidProductExists.IsActive = true;
                await _licenseRepository.UpdateAsync(IsValidProductExists);
                response.IsSuccessful = true;
                response.Message = "Product activated successfuly";
            }
            else
            {
                response.IsSuccessful = false;
                response.Message = "Product activation failed";
            }
            //_emailSender.SendEmail("code.shakir@gmail.com", user.Email, "License key Updated Successfuly", $"Dear User please note that you product license key has been updated, you new key is {newGuid}");
        }
        return response;
    }

    public async Task<List<LicenseResponseDTO>> GetLicensesForAdmin(string userid)
    {
        return GetAllLicenses(userid, true);
    }
    public async Task<List<LicenseResponseDTO>> GetLicensesForUser(string userid)
    {
        return GetAllLicenses(userid, false);
    }

    private List<LicenseResponseDTO> GetAllLicenses(string userid, bool isAdmin)
    {
        var reponse = _licenseRepository.GetAllAsync().Include(e=>e.Product).Include(u=>u.User).Where(x => x.UserId == userid || isAdmin).Select(x => new LicenseResponseDTO
        {
            Key =x.Id,
            ProductName = x.Product.ProductName,
            UserName = x.User.UserName,
            IsActive = x.IsActive,
        }).ToList();
        return reponse;
    }



    public async Task<LicenseDTO> GetLicenseByIdAsync(long licenseId)
    {
        var entity = await _licenseRepository.GetByIdAsync(licenseId);
        return new LicenseDTO
        {

        };
    }

    public async Task UpdateLicenseAsync(LicenseDTO license)
    {
        await _licenseRepository.UpdateAsync(new License { }); /// to be Updated latter. 
    }

    public async Task DeleteLicenseAsync(long licenseId)
    {
        var license = await _licenseRepository.GetByIdAsync(licenseId);
        if (license != null)
        {
            await _licenseRepository.DeleteAsync(license);
        }
    }


}
