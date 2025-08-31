using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreMVC.Models.View
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal CartTotal { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Total => CartTotal + ShippingCost;
    }
}