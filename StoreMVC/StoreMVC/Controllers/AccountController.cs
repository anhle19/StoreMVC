using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using StoreMVC.Models;
using StoreMVC.Models.Identity;
using StoreMVC.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace StoreMVC.Controllers
{
    public class AccountController : Controller
    {
        private AppUserManager _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Nếu đã đăng nhập thì chuyển hướng về trang chủ
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Title = "Login page";
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                var appDbContext = new AppDbContext();
                var userStore = new AppUserStore(appDbContext);
                var userManager = new AppUserManager(userStore);
                var user = userManager.Find(loginVM.Email, loginVM.Password);

                if (user != null)
                {
                    var authenManager = HttpContext.GetOwinContext().Authentication;
                    // Tạo cookie
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenManager.SignIn(new AuthenticationProperties(), userIdentity);

                    return RedirectToAction("Index","Home");
                }
                ModelState.AddModelError("New Error", "Login Failt");
                return View();
            }
            else
            {
                ModelState.AddModelError("New Error", "Invalid Data");
                return View();
            }
        }

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Nếu đã đăng nhập thì chuyển hướng về trang chủ
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Title = "Register page";
            return View();
        } 

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                var appDbContext = new AppDbContext();
                var userStore = new AppUserStore(appDbContext);
                var userManager = new AppUserManager(userStore);
                var password = registerVM.Password;

                var user = new AppUser();
                user.UserName = registerVM.Email;
                user.FullName = registerVM.FullName;
                user.Email = registerVM.Email;

                // Tạo account với role là Customer
                var checkUser = userManager.Create(user, password);
                if (checkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");

                    // Xử lý đăng nhập dạng Cookie
                    var authenManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenManager.SignIn(new AuthenticationProperties(), userIdentity);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("New Error", "Register Failt");
                return View();
            }
            else
            {
                ModelState.AddModelError("New Error", "Invalid Data");
                return View();
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}