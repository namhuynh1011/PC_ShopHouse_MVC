﻿using System.ComponentModel.DataAnnotations;

namespace PC_ShopHouse.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string BrandName { get; set; }
        public List<Product>? Products { get; set; }
    }
}
