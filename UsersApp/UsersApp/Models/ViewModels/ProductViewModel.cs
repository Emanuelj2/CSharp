using System.ComponentModel.DataAnnotations;

namespace UsersApp.Models.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public string? ImageUrl { get; set; }
        public ProductCategory Category { get; set; } = ProductCategory.Electronics; // Default category
    }
}
