namespace Edunext_Model.DTOs.Cart
{
    public class ShoppingCartDTO
    {
        public CartOrderDTO Order { get; set; }

        public IEnumerable<CartOrderDetailDTO> OrderDetails { get; set; }
    }
}
