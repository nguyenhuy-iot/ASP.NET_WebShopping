using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShopping.Models
{
    [Serializable]
    public class CartItem
    {
        public int ProducID { get; set; }
        public int Quantity { get; set; }
    }
}