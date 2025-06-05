using PC_ShopHouse.Models;
using System.ComponentModel.DataAnnotations;

namespace PC_ShopHouse.ViewModels
{
    public class ProductCreateViewModel
    {
        public Product Product { get; set; } = new Product();

        public IFormFile? ImageFile { get; set; }

        public IEnumerable<Category>? Categories { get; set; }
        public IEnumerable<Brand>? Brands { get; set; }

        // Thêm các trường chi tiết linh kiện 
        public CPU? CPU { get; set; }
        public Mainboard? Mainboard { get; set; }
        public GPU? GPU { get; set; }
    }
}
