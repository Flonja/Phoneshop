using Phoneshop.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Phoneshop.Api.DTO
{
    public class PhoneDTO
    {
        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Url]
        public string Image { get; set; }
    }
}