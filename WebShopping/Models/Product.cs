using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShopping.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Quantity in stock")]
        public int QuantityInStock { set; get; }

        [DisplayName("Unit Price")]
        public decimal UnitPrice { set; get; }

        [DisplayName("Category")]
        public int CategoryID { get; set; }

        //
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}