using System.ComponentModel.DataAnnotations;

namespace PC_ShopHouse.Models
{
    public class CheckoutViewModel
    {
        [Required]
        [Display(Name = "Họ tên")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Địa chỉ giao hàng")]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
    }
}
