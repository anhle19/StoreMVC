using Microsoft.AspNet.Identity;
using StoreMVC.Helpers;
using StoreMVC.Models;
using StoreMVC.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StoreMVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            try
            {
                var cart = CartHelper.GetCart(Session);

                // Kiểm tra giỏ hàng
                if (cart == null || !cart.Any())
                {
                    return RedirectToAction("Index", "Cart");
                }

                // Tạo view model
                var orderVM = new OrderViewModel() 
                { 
                    CartItems = cart,
                    ShippingCost = 30000,
                };

                ViewBag.Title = "Order page";
                return View(orderVM);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Order(OrderViewModel orderVM)
        {
            try
            {
                var cart = CartHelper.GetCart(Session);

                // Kiểm tra giỏ hàng
                if (cart == null || !cart.Any())
                {
                    return RedirectToAction("Index", "Cart");
                }

                if (ModelState.IsValid)
                {
                    // Tạo Order
                    var order = new Order() 
                    {
                        UserId = User.Identity.GetUserId(),
                        OrderDate = DateTime.Now,
                        FullName = orderVM.FullName,
                        Email = orderVM.Email,
                        Phone = orderVM.Phone,
                        Address = orderVM.Address,
                        Status = OrderStatus.Pending,
                        ShippingCost = 30000,
                        OrderDetails = new List<OrderDetail>()
                    };

                    decimal subTotal = 0;

                    // Tạo OrderDetail
                    foreach (var item in cart)
                    {
                        var orderDetail = new OrderDetail()
                        {
                            OrderId = order.Id,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Price = item.Price,
                            Quantity = item.Quantity,
                        };
                        subTotal += item.Total;
                        order.OrderDetails.Add(orderDetail);
                    }

                    // Lưu Order vào CSDL
                    order.Total = subTotal + order.ShippingCost;
                    db.Orders.Add(order);
                    await db.SaveChangesAsync();

                    // Xóa Cart trong Session
                    CartHelper.ClearCart(Session);
                    
                    return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError("", "Invalid Data");
                orderVM.CartItems = cart;
                return View("Index",orderVM);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}