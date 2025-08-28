using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreMVC.Models.ViewModels
{
    public class HomeView
    {
        public List<Category> Categories { get; set; }
        public List<Product> AllProducts { get; set; }
        public List<Product> FeaturedProducts { get; set; }
    }
}