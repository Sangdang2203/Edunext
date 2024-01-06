using System.ComponentModel.DataAnnotations;

namespace Edunext_Model.Models
{
    public class Order: IValidatableObject
    {
        [Key]
        public required string Id { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        [Required]
        public required string ShipWard { get; set; }

        [Required]
        public required string ShipDitrict { get; set; }

        [Required]
        public required string ShipCity { get; set; }

        [Required]
        [RegularExpression("^(Pending|Shipped|Completed|Cancelled|Declined|Refunded|)$", ErrorMessage = "Invalid role!")]
        public required string Status { get; set; }

        public string? Comment { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? DateUpdate { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; } = null!;

<<<<<<< HEAD
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
=======
        public ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
>>>>>>> ac65bf5eecdb2ce81e28635fcb992174033ade82

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (RequiredDate <= OrderDate)
            {
                yield return new ValidationResult(
                    $"Required date must be later than { OrderDate }.",
                    new[] { nameof(RequiredDate) });
            }
        }
    }
}
