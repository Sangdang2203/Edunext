using Edunext_Model.Models;

namespace Edunext_Model.DTOs.Product
{
    public class ProductGet
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public required string Description { get; set; }

        public int CategoryId { get; set; }
        public CategoryGet? Category { get; set; }
    }
}
