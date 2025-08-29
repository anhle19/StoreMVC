using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Threading.Tasks;
using StoreMVC.Models;
using StoreMVC.Models.Identity;

[assembly: OwinStartup(typeof(StoreMVC.Startup))]

namespace StoreMVC
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            CreateRolesAndUsers();
        }

        public void CreateRolesAndUsers()
        {
            var appDbContext = new AppDbContext();

            // Quản lý Roles
            var roleManager = new RoleManager<IdentityRole> (new RoleStore<IdentityRole> (new AppDbContext()));

            // Quản lý Users 
            var userStore = new AppUserStore(appDbContext);
            var userManager = new AppUserManager(userStore);

            // Tạo role Admin nếu chưa có role
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            // Tạo account Admin nếu chưa có
            if (userManager.FindByName("Admin") == null)
            {
                var user = new AppUser();
                user.UserName = "Admin";
                user.Email = "admin@gmail.com";
                string userPwd = "admin123";

                // Kiểm tra quá trình tạo và thêm role cho account
                var checkUser = userManager.Create(user, userPwd);
                if (checkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            // Tạo role Customer nếu chưa tồn tại
            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }

        }
    }
}
