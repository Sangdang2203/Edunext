using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edunext_Model.Models
{
    public class Order: IValidatableObject
    {
        public Order()
        {
            Status = "Pending";
            OrderDate = DateTime.Now;
            DateUpdate = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        [Required]
        public required string ShipWard { get; set; }

        [Required]
        public required string ShipDitrict { get; set; }

        [Required]
        public required string ShipCity { get; set; }

        [Required]
        [RegularExpression("^(Pending|Shipped|Completed|Cancelled|Declined|Refunded|)$", ErrorMessage = "Invalid status!")]
        public required string Status { get; set; }

        public string? Comment { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DateUpdate { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (RequiredDate <= OrderDate)
            {
                yield return new ValidationResult(
                    $"Required date must be later than { OrderDate }.",
                    new[] { nameof(RequiredDate) });
            }

            if (ShippedDate <= OrderDate)
            {
                yield return new ValidationResult(
                    $"Shipped date must be later than {OrderDate}.",
                    new[] { nameof(ShippedDate) });
            }
        }
    }
}
