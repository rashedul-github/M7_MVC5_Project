using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMVCProject.Controllers
{
    [Authorize]
    public class CheckAvailabilitiesController : Controller
    {
        // GET: CheckAvailabilities
        CarDbContext db = new CarDbContext();
        public ActionResult Index()
        {
            return View(db.CheckAvailabilities.ToList());
        }
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.CarList = db.Cars.ToList();
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(CheckAvailability c)
        {
            if (ModelState.IsValid)
            {
                db.CheckAvailabilities.Add(c);
                db.SaveChanges();
                return RedirectToAction("success");
            }
            else
            {
                ModelState.AddModelError("", "Fail to insert.");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.CarList = db.Cars.ToList();
            var data = db.CheckAvailabilities.First(x => x.Id == id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(CheckAvailability c)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }
        public ActionResult Delete(int id)
        {
            var data = db.CheckAvailabilities.First(x => x.Id == id);
            return View(data);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            if (ModelState.IsValid)
            {
                var a = new CheckAvailability { Id = id };
                db.Entry(a).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(id);
        }
        [AllowAnonymous]
        public ActionResult success()
        {
            return View();
        }
    }
}