using System.ComponentModel.DataAnnotations;

namespace Phoneshop.Api.DTO
{
    public class UserRegisterDTO : UserAuthenticationDTO
    {
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
