using System.ComponentModel.DataAnnotations;

namespace PC_ShopHouse.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string CategoryName { get; set; }
        public List<Product>? Products { get; set; }
    }
}
