namespace Edunext_Model.Models
{
    public class User
    {
        public ICollection<Order>? Orders { get; set; }
    }
}
