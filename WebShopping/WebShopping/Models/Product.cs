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
        public int ProductID { get; set; }

        [StringLength(100)]
        public string image { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Tên hàng bắt buộc phải nhập.")]
        [StringLength(100)]
        public string ProductName { get; set; }

        [DisplayName("Quantity in stock")]
        public int QuantityInStock { set; get; }

        [DisplayName("Unit Price")]
        public float UnitPrice { set; get; }

        [DisplayName("Category")]
        public int CategoryID { get; set; }

        //
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}