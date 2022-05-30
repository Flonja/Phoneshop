using System.ComponentModel.DataAnnotations;

namespace Phoneshop.Domain.Models
{
    public class LoginInputModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(16, ErrorMessage = "Must be between 6 and 16 characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirming your password is required.")]
        [StringLength(16, ErrorMessage = "Must be between 6 and 16 characters.", MinimumLength = 6)]
        [Compare(nameof(Password), ErrorMessage = "Your passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
