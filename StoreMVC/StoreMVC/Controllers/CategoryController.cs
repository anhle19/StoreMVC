using StoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreMVC.Controllers
{
    public class CategoryController : Controller
    {
        private AppDbContext db = new AppDbContext();

        [ChildActionOnly]
        public PartialViewResult _CategoryNavbar()
        {
            List<Category> categories = db.Categories.ToList();
            return PartialView(categories);
        }
    }
}