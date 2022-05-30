using System.ComponentModel.DataAnnotations;

namespace Phoneshop.Api.DTO
{
    public class UserAuthenticationDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
