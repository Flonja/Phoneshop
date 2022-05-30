using System.ComponentModel.DataAnnotations;

namespace Phoneshop.Api.DTO
{
    public class PhoneCreateDTO : PhoneDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Brand { get; set; }
    }
}