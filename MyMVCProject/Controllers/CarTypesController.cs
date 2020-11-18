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
    public class CarTypesController : Controller
    {
        // GET: CarTypes
        CarDbContext db = new CarDbContext();
        public ActionResult Index(int page=1)
        {
            int perPage = 3;
            var data = db.carTypes
                .OrderBy(x => x.TypeId)
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)db.carTypes.Count() / perPage);
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public PartialViewResult Create(CarType ct)
        {
            if (ModelState.IsValid)
            {
                db.carTypes.Add(ct);
                db.SaveChanges();
                return PartialView("_success");
            }
            else
            {
                return PartialView("_fail");
            }
        }
        public ActionResult Edit(int id)
        {
            var data = db.carTypes.First(x => x.TypeId == id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(CarType t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t);
        }
        public ActionResult Delete(int id)
        {
            var data = db.carTypes.First(x => x.TypeId == id);
            return View(data);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            if (ModelState.IsValid)
            {
                var ct = new CarType { TypeId = id };
                db.Entry(ct).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(id);
        }

    }
}