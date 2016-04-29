using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace I_Shop.Models.ShopModels
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime dateTime { get; set; }
        public ICollection<Product> Product { get; set; }
        public Customer Customer { get; set; }
    }
}