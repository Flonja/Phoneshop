using System.ComponentModel.DataAnnotations;

namespace Phoneshop.Api.DTO
{
    public class PhoneUpdateDTO : PhoneDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
