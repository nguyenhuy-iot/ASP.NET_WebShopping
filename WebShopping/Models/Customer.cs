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
        public string Name { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Phone")]
        public string Phone { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        //
        public virtual ICollection<Order> Orders { get; set; }
        
    }
}