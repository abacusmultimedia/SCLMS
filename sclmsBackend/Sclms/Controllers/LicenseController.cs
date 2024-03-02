using Sclms.DTOS;
using Sclms.UseCases.IServices;
using Microsoft.AspNetCore.Mvc;
using Sclms.Util;

[ApiController]
[Route("api/licenses")]
public class LicenseController : ControllerBase
{
    private readonly ILicenseService _licenseService;
    private readonly CurrentUserInfo _currentUser;
    public LicenseController(ILicenseService licenseService)
    {
        _licenseService = licenseService;
        _currentUser = new CurrentUserInfo();
    }

    [HttpGet("{licenseId}")]
    public async Task<IActionResult> GetLicense(int licenseId)
    {
        var license = await _licenseService.GetLicenseByIdAsync(licenseId);
        if (license == null)
        {
            return NotFound();
        }
        return Ok(license);
    }

    [HttpPost]
    public async Task<IActionResult> AddLicense([FromBody] LicenseDTO license)
    {

        var Response = await _licenseService.AddLicenseAsync(license);
        return Ok(new { licenseIdKey = Response.Message });
    }
    [HttpPost("ValidateProdcut")]
    public async Task<IActionResult> ValidateProdcut([FromBody] LicenseActivateionDTO license)
    {

        var Response = await _licenseService.LicenseActivationAsync(license);
        return CreatedAtAction("GetAll", new { licenseIdKey = Response }, license);
    }

    // Get License for Admin
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllLicenses()
    {
        var currentUser = _currentUser.GetUser(Request.Headers);
        var Role = _currentUser.GetRole(Request.Headers);
        if (Role == "Admin")
        {
            var response = await _licenseService.GetLicensesForAdmin(currentUser);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
        return NotFound();
    }


    // Get my License for User
    [HttpGet("GetMyLicenses")]
    public async Task<IActionResult> GetMyLicenses()
    {
        var currentUser = _currentUser.GetUser(Request.Headers);
        var response = await _licenseService.GetLicensesForUser(currentUser);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response);
    }
    //[HttpPut("{licenseId}")]
    //public async Task<IActionResult> UpdateLicense(int licenseId, [FromBody] LicenseDTO license)
    //{
    //    if (licenseId != license.Id)
    //    {
    //        return BadRequest();
    //    }

    //    await _licenseService.UpdateLicenseAsync(license);
    //    return NoContent();
    //}

    [HttpDelete("{licenseId}")]
    public async Task<IActionResult> DeleteLicense(int licenseId)
    {
        await _licenseService.DeleteLicenseAsync(licenseId);
        return NoContent();
    }
}
