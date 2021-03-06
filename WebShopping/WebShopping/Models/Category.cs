﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShopping.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Required()]
        [DisplayName("Category Name")]
        [StringLength(100)]
        public string Name { get; set; }

        //
        public virtual ICollection<Product> Products { get; set; }


    }
}