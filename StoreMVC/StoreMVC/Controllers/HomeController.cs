using StoreMVC.Models;
using StoreMVC.Models.View;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();
        public async Task<ActionResult> Index()
        {
            try
            {
                List<Category> categories = await db.Categories.ToListAsync();
                List<Product> allProducts = await db.Products
                                    .Where(p => p.IsFeatured == false)
                                    .Take(8)
                                    .ToListAsync();

                List<Product> featuredProduct = await db.Products
                                        .Where(p => p.IsFeatured == true)
                                        .Take(4)
                                        .ToListAsync();

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