using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShopping.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [DisplayName("Address")]
        [StringLength(100)]
        public string Address { get; set; }

        [DisplayName("Phone")]
        [StringLength(20)]
        public string Phone { get; set; }

        [DisplayName("Email")]
        [StringLength(100)]
        public string Email { get; set; }

        //
        public virtual ICollection<Order> Orders { get; set; }
        
    }
}