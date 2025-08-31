using StoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace StoreMVC.Controllers
{
    public class StoreController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();
        private readonly int pageSize = 5;

        public async Task<ActionResult> Index(int? page)
        {
            try
            {
                // Mặc định page là 1
                int pageNumber = (page ?? 1);

                List<Product> products = await db.Products.OrderBy(p => p.Id)
                                        .Skip((pageNumber - 1) * pageSize)//Bỏ qua sản phẩm của các trang trước
                                        .Take(pageSize)
                                        .ToListAsync();

                int totalProduct = await db.Products.CountAsync();

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

        public async Task<ActionResult> StoreByCategory(int categoryId, int? page)
        {
            try
            {
                int pageNumber = (page ?? 1);

                // Truy vấn
                var query = db.Products.Where(p => p.CategoryId == categoryId);

                int totalProduct = await query.CountAsync();

                List<Product> products = await query.OrderBy(p => p.Id)
                                                    .Skip((pageNumber - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToListAsync();

                Category category = await db.Categories
                                            .FirstOrDefaultAsync(c => c.Id == categoryId);

                if (category == null)
                {
                    ViewBag.Error = "Danh mục không tồn tại.";
                    return View("Error");
                }

                // Thông tin bổ sung cho View
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalProduct / pageSize);
                ViewBag.CurrentPage = pageNumber;
                ViewBag.CategoryId = categoryId;
                ViewBag.CategoryName = category.Name;
                ViewBag.Title = "Store page - " + category.Name;

                return View("StoreByCategory", products);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Có lỗi xảy ra: " + ex.Message;
                return View("Error");
            }
        }


        public async Task<ActionResult> Detail(int id)
        {
            try
            {
                Product product = await db.Products.FindAsync(id);

                if (product == null)
                {
                    return View("Error");
                }

                ViewBag.Title = "Product detail page";
                return View(product);
            }
            catch (Exception ex)
            {
                // Có thể log lỗi ra file hoặc DB
                ViewBag.Error = "Có lỗi xảy ra: " + ex.Message;
                return View("Error");
            }
        }

    }
}