using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edunext_Model.Models;

namespace Edunext_Model.DTOs.Cart
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal LineTotal { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProductName { get; set; }
        public string? ProductImage { get; set; }
        public string Status { get; set; }
    }
}
