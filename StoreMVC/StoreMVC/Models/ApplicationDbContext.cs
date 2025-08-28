using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StoreMVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("StoreMVC")
        {

        }

        //Ánh xạ đến các bảng tương ứng trong CSDL
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}