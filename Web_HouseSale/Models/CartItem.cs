using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_HouseSale.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public string Color { get; set; }
        public string Storage { get; set; }
        public int Quantity { get; set; }
    }
}
