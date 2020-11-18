
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MyMVCProject.Models;
using MyMVCProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMVCProject.Controllers
{
    public class AccountsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var authManager = HttpContext.GetOwinContext().Authentication;
            var user = userManager.Find(model.Username, model.Password);
            if (user != null)
            {
                var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterVM model)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = new ApplicationUser { UserName = model.Username };
            var r = userManager.CreateAsync(user, model.Password);
            if (r.Result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}