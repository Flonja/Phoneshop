using System.ComponentModel.DataAnnotations;

namespace Phoneshop.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
