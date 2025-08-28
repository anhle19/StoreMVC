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
        private ApplicationDbContext db = new ApplicationDbContext();

        [ChildActionOnly]
        public PartialViewResult _CategoryNavbar()
        {
            List<Category> categories = db.Categories.ToList();
            return PartialView(categories);
        }
    }
}