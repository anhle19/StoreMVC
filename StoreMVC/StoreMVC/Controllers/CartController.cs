using StoreMVC.Helpers;
using StoreMVC.Models;
using StoreMVC.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace StoreMVC.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            var cart = CartHelper.GetCart(Session);

            var cartVM = new CartViewModel()
            {
                CartItems = cart,
                CartTotal = CartHelper.GetTotal(Session),
                ShippingCost = 30000
            };
            ViewBag.Title = "Cart page";
            
            return View(cartVM);
        }

        public PartialViewResult _Cart()
        {
            ViewBag.CartCount = CartHelper.GetCartCount(Session);
            return PartialView();
        }

        [HttpPost]
        public ActionResult Add(int id, string name, string image, decimal price, int quantity)
        {
            try
            {
                var item = new CartItem()
                {
                    ProductId = id,
                    ProductName = name,
                    ProductImage = image,
                    Price = price,
                    Quantity = quantity
                };

                CartHelper.AddToCart(Session, item);
                return RedirectToAction("Index", "Cart");
            }
            catch
            {
                return View("Error");
            }
            
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int productId, int quantity)
        {
            try
            {
                CartHelper.UpdateQuantity(Session, productId, quantity);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }


        public ActionResult Remove(int id)
        {
            try
            {
                CartHelper.RemoveFromCart(Session, id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult Clear()
        {
            CartHelper.ClearCart(Session);
            return RedirectToAction("Index");
        }
    }
}