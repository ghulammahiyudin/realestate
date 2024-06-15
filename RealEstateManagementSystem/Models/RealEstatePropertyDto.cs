using System.ComponentModel.DataAnnotations;

namespace RealEstateManagementSystem.Models
{
    public class RealEstatePropertyDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required ,MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public decimal Price { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
