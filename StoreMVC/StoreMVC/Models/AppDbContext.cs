using Microsoft.AspNet.Identity.EntityFramework;
using StoreMVC.Models.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StoreMVC.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() : base("StoreMVC")
        {

        }

        // Factory method tạo ra ApplicationDbContext mỗi khi cần
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }


        //Ánh xạ đến các bảng tương ứng trong CSDL
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}