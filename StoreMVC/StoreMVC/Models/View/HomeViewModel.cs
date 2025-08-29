using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreMVC.Models.View
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> AllProducts { get; set; }
        public List<Product> FeaturedProducts { get; set; }
    }
}