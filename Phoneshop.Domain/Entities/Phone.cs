using System.ComponentModel.DataAnnotations;

namespace Phoneshop.Domain.Entities
{
    public class Phone
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int Stock { get; set; }
        [Required]
        public double Price { get; set; }
        public string Image { get; set; } = "assets/icon.png";

        public string GetStockInfo()
        {
            if (Stock <= 5)
            {
                return $"Almost out of stock! Only {Stock} left!";
            }

            return $"{Stock} in stock.";
        }
    }
}
