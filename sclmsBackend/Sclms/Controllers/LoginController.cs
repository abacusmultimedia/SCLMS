using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Sclms.DTOS.Login;
using Sclms.Persistence.Modles;
using Azure.Core;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly SignInManager<AppUsers> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUsers> _userManager;
    public LoginController(SignInManager<AppUsers> signInManager, IConfiguration configuration, UserManager<AppUsers> userManager)
    {
        _signInManager = signInManager;
        _configuration = configuration;
        _userManager = userManager;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<AuthorizationResponse>> Login([FromBody] LoginDTO model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var user = await _signInManager.UserManager.FindByNameAsync(model.Username);
            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user.Id, user.UserName, roles.Any(x=>x == "Admin"));

            return new AuthorizationResponse
            {
                Token = token,
                Roles = roles.ToList(),
                User = token,
                AccessToken = token,
                UserId = user.Id,
                UserName = user.UserName,
                IsAuthSucessful = true 
            };
        }

        return Unauthorized();
    }

    private string GenerateJwtToken(string userId, string username, bool isAdmin)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("R7DmWIE0FzL1UfvP5jgOUdKRkGidt3Wgl6N+T2m4psk="));

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role,  isAdmin? "Admin":"User" )
            // Add additional claims as needed
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["https://localhost:7274"],
            audience: _configuration["http://localhost:4200"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1), // Token expiration time
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO model)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUsers { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok("Registration successful");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return BadRequest(ModelState);

    }
}
