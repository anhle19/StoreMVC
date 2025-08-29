using StoreMVC.Models;
using StoreMVC.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db = new AppDbContext();
        public ActionResult Index()
        {
            try
            {
                List<Category> categories = db.Categories.ToList();
                List<Product> allProducts = db.Products
                                    .Where(p => p.IsFeatured == false)
                                    .Take(8)
                                    .ToList();

                List<Product> featuredProduct = db.Products
                                        .Where(p => p.IsFeatured == true)
                                        .Take(4)
                                        .ToList();

                HomeViewModel homeView = new HomeViewModel
                {
                    Categories = categories,
                    AllProducts = allProducts,
                    FeaturedProducts = featuredProduct
                };

                ViewBag.Title = "Home page";
                return View(homeView);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}