using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace demo_project.Models
{
    public class Contex : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }

    }
}










