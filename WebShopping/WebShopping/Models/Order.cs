using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShopping.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; } 

        [DisplayName("Customer")]
        public int CustomerID { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        [Required()]
        [DisplayName("Requre Date")]
        public DateTime RequreDate { get; set; }

        [DisplayName("Ship Address")]
        [StringLength(100)]
        public string ShipAddress { get; set; }

        [Required()]
        [StringLength(20)]
        public string Phone { get; set; }

        //
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { set; get; }

        public virtual ICollection<OrderDetail> OrderDetails { set; get; }

    }
}