using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PC_ShopHouse.Models
{
    public class Mainboard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [Required, StringLength(100)]
        public string Chipset { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Socket { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string FormFactor { get; set; } = string.Empty; // VD: ATX, mATX, ITX

        [Required, StringLength(50)]
        public string RAMType { get; set; } = string.Empty; // DDR4, DDR5

        [Required]
        public int MaxRAM { get; set; } // Dung lượng RAM tối đa hỗ trợ (GB)

        [Required]
        public int RAMSlots { get; set; } // Số khe RAM

        [Required]
        public int PCIESlots { get; set; } // Số khe PCIe

        [Required]
        public int SATA_Ports { get; set; } // Số cổng SATA

        [Required]
        public int M2Slots { get; set; } // Số khe M.2
    }
}
