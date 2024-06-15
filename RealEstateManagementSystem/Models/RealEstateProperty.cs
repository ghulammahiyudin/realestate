using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RealEstateManagementSystem.Models
{
    public class RealEstateProperty
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public string Type { get; set; }

        [Precision(16, 2)]
        public decimal Price { get; set; }

        public String Image {  get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
