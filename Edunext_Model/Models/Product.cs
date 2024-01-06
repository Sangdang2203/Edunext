using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunext_Model.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryID { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
