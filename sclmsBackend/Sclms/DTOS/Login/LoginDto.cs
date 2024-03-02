using System.ComponentModel.DataAnnotations;

namespace Sclms.DTOS.Login
{
    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }


    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class AuthorizationResponse
    {
        public string Token { get; set; }
        public string? User  { get; set; }
        public string? UserId { get; set; }
        public string AccessToken { get; set; }
        public string? UserName { get; set; }
        public List<string>? Roles { get; set; }
        public bool IsAuthSucessful  { get; set; }
    }

}

