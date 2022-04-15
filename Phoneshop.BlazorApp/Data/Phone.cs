namespace Phoneshop.BlazorApp.Data
{
    public class Phone
    {
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public string Image { get; set; } = "assets/icon.png";
    }
}
