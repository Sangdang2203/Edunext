namespace Edunext_Model.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public string OrderId { get; set; }

        public Order? Order { get; set; }
    }
}
