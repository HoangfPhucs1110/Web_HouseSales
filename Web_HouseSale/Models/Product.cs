using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_HouseSale.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Category { get; set; }

        [MaxLength(255)]
        public string Slug { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal? OldPrice { get; set; }

        public int Stock { get; set; }

        public bool IsNew { get; set; }

        public int? SalePercent { get; set; }

        // --- Dữ liệu từ View ---
        public string ShortDesc { get; set; }        // Mô tả ngắn
        public string Description { get; set; }      // Mô tả chi tiết

        // --- Ảnh ---
        public string ImageUrl { get; set; }         // Ảnh chính
        public string ImageUrl2 { get; set; }        // Ảnh phụ 1 (optional)
        public string ImageUrl3 { get; set; }        // Ảnh phụ 2 (optional)
        public string ImageUrl4 { get; set; }        // Ảnh phụ 3 (optional)

        // --- Tùy chọn ---
        public string Colors { get; set; }           // Dạng CSV: "Black,White,Red"
        public string Sizes { get; set; }            // Dạng CSV: "128GB,256GB,512GB"
    }
}
