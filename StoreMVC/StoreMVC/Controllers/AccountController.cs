using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using StoreMVC.Models;
using StoreMVC.Models.Identity;
using StoreMVC.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace StoreMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppUserManager _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

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
        public async Task<ActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                var appDbContext = new AppDbContext();
                var userStore = new AppUserStore(appDbContext);
                var userManager = new AppUserManager(userStore);

                // Kiểm tra user (async)
                var user = await userManager.FindAsync(loginVM.Email, loginVM.Password);

                if (user != null)
                {
                    var authenManager = HttpContext.GetOwinContext().Authentication;

                    // Tạo cookie (async)
                    var userIdentity = await userManager.CreateIdentityAsync(
                        user,
                        DefaultAuthenticationTypes.ApplicationCookie
                    );

                    authenManager.SignIn(new AuthenticationProperties(), userIdentity);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Login Failed");
                return View(loginVM);
            }

            ModelState.AddModelError("", "Invalid Data");
            return View(loginVM);
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
        public async Task<ActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                var appDbContext = new AppDbContext();
                var userStore = new AppUserStore(appDbContext);
                var userManager = new AppUserManager(userStore);

                var password = registerVM.Password;

                var user = new AppUser
                {
                    UserName = registerVM.Email,
                    FullName = registerVM.FullName,
                    Email = registerVM.Email
                };

                // Tạo account với role là Customer (async)
                var checkUser = await userManager.CreateAsync(user, password);
                if (checkUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user.Id, "Customer");

                    // Xử lý đăng nhập dạng Cookie (async)
                    var authenManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = await userManager.CreateIdentityAsync(
                        user,
                        DefaultAuthenticationTypes.ApplicationCookie
                    );
                    authenManager.SignIn(new AuthenticationProperties(), userIdentity);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Register Failed");
                return View(registerVM);
            }

            ModelState.AddModelError("", "Invalid Data");
            return View(registerVM);
        }


        [Authorize]
        public ActionResult Logout()
        {
            var authenManager =  HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index","Home");
        }
    }
}