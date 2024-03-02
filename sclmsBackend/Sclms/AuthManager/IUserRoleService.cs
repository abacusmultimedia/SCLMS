using System;
using System.Threading.Tasks;
using Sclms.Persistence.Modles;
using Microsoft.AspNetCore.Identity;

public interface IUserRoleService
{
    Task<bool> AddUserToRoleAsync(string userId, string roleName);
}

public class UserRoleService : IUserRoleService
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserRoleService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<bool> AddUserToRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            // Handle the case where the user with the specified ID is not found
            return false;
        }

        // Check if the role exists, and create it if it doesn't
        //var roleExists = await _userManager.RoleExistsAsync(roleName);

        //if (!roleExists)
        //{
        //    await _userManager.CreateAsync(new IdentityRole(roleName));
        //}

        // Assign the user to the role
        var result = await _userManager.AddToRoleAsync(user, roleName);

        // Return true if the operation was successful
        return result.Succeeded;
    }
}
