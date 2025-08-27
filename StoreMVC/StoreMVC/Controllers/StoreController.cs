using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreMVC.Controllers
{
    public class StoreController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Store page";
            return View("Store");
        }

        public ActionResult Detail()
        {
            ViewBag.Title = "Product detail page";
            return View("Detail");
        }

        public ActionResult ShoppingCart() 
        {
            ViewBag.Title = "Shopping cart page";
            return View("ShoppingCart");
        }

        public ActionResult CheckOut()
        {
            ViewBag.Title = "Checkout page";
            return View("CheckOut");
        } 
    }
}