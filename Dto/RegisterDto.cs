using System.ComponentModel.DataAnnotations;

namespace FitFormServer.Dto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "First name is required!")]
        public required string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required!")]
        public required string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Invalid email format!")]
        public required string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required!")]
        public required string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User";
    }
}
