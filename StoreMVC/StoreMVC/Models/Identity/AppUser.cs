using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreMVC.Models.Identity
{
    public class AppUser: IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}