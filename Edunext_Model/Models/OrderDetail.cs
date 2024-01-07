namespace Edunext_Model.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Order? Order { get; set; }
    }
}
