using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edunext_API.Models
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailID { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        [Range(1, 1000,ErrorMessage = "Quantity must be between 1 and 1000")]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.1, 2000, ErrorMessage = "Price must be between 0.1 and 2000")]
        public decimal UnitPrice { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.1, 100, ErrorMessage = "Discount must be between 0.1 and 100")]
        public decimal Discount { get; set; }
        public decimal LineTotal
        {
            get
            {
                return Quantity * UnitPrice * (1 - Discount / 100);
            }
        }
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public Order? Order { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }
    }
}
