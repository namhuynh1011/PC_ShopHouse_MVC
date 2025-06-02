using System.ComponentModel.DataAnnotations;

namespace PC_ShopHouse.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public List<ProductImage>? Images { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
    }
}
