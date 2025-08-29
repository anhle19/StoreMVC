using StoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace StoreMVC.Controllers
{
    public class StoreController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private readonly int pageSize = 5;

        public ActionResult Index(int? page)
        {
            try
            {
                // Mặc định page là 1
                int pageNumber = (page ?? 1);

                List<Product> products = db.Products.OrderBy(p => p.Id)
                                        .Skip((pageNumber - 1) * pageSize)//Bỏ qua sản phẩm của các trang trước
                                        .Take(pageSize)
                                        .ToList();

                int totalProduct = db.Products.Count();

                //Lấy tổng số trang và trang hiện tại
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalProduct / pageSize);
                ViewBag.CurrentPage = pageNumber;

                ViewBag.Title = "Store page";
                return View(products);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Có lỗi xảy ra: " + ex.Message;
                return View("Error");
            }
        }

        public ActionResult StoreByCategory(int categoryId, int? page)
        {
            try
            {
                int pageNumber = (page ?? 1);

                // Truy vấn
                var query = db.Products.Where(p => p.CategoryId == categoryId);
                int totalProduct = query.Count();

                List<Product> products = query.OrderBy(p => p.Id)
                                         .Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize).ToList();
                Category category = db.Categories.FirstOrDefault(c => c.Id == categoryId);

                // Thông tin bổ sung cho View
                ViewBag.TotalPages = (int)Math.Ceiling((double) totalProduct / pageSize);
                ViewBag.CurrentPage = pageNumber;
                ViewBag.CategoryId = categoryId;
                ViewBag.CategoryName = category.Name;
                ViewBag.Title = "Store page - " + category.Name;

                return View("StoreByCategory", products);
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Có lỗi xảy ra: " + ex.Message;
                return View("Error");
            }
        }

        public ActionResult Detail(int id)
        {
            try
            {
                Product product = db.Products.Find(id);
                ViewBag.Title = "Product detail page";
                return View(product);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}