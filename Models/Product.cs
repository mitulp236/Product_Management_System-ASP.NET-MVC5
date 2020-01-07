using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo_project.Models
{
    public class Product
    {
        
        public int ProductId { get; set; }

        [Required]
        public String ProductName { get; set; }
        [Required]
        public String Category { get; set; }
        [Required]
        public Double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public String Sdescription { get; set; }
        [Required]
        public String Ldescription { get; set; }
        
        public String Simage { get; set; }
        
        public String Limage { get; set; }


    }
}