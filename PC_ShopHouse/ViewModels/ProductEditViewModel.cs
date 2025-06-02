using Microsoft.AspNetCore.Mvc.Rendering;
using PC_ShopHouse.Models;

namespace PC_ShopHouse.ViewModels
{
    public class ProductEditViewModel
    {
        public Product Product { get; set; }
        public CPU? CPU { get; set; }
        // Các loại linh kiện khác
        public IEnumerable<SelectListItem>? Categories { get; set; }
        public IEnumerable<SelectListItem>? Brands { get; set; }
    }
}
