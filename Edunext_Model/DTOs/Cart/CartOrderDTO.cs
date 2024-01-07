namespace Edunext_Model.DTOs.Cart
{
    public class CartOrderDTO
    {
        public DateTime? RequiredDate { get; set; }

        public string? ShipWard { get; set; }

        public string? ShipDitrict { get; set; }

        public string? ShipCity { get; set; }

        public string? Comment { get; set; }
    }
}
