
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Edunext_Model.DTOs.Product
{
    public class ProductPost
    {
        [Required]
        public required string Code { get; set; }

        [Required]
        [StringLength(20)]
        public required string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Price { get; set; }

        [Required]
        public int? Quantity { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required IFormFile Image {  get; set; } 

        [Required]
        public int? CategoryId { get; set; }
    }
}
