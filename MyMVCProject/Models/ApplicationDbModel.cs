using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMVCProject.Models
{
    public class ApplicationUser : IdentityUser
    {
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

    }
}