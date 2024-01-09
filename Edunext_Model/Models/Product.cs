using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Edunext_Model.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Code { get; set; }

        [Required]
        [StringLength(20)]
        public required string Name { get; set; }

        [Required]
        public required string ImageUrl { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; } 

        [Required]
        public required string Description { get; set; }

        [Required]
        [NotMapped]
        public required IFormFile Image { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
        [JsonIgnore]
        public ICollection<OrderDetail>? OrderDetails { get; set; }

    }
}