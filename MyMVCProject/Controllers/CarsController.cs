using ClassLibrary;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace MyMVCProject.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        // GET: Cars
        CarDbContext db = new CarDbContext();
        public ActionResult Index(int page=1)
        {
            int perPage = 4;
            var data = db.Cars
                .OrderBy(x => x.CarId)
                .ToPagedList(page, perPage);
            return View(data);
        }
        public ActionResult Create()
        {
            ViewBag.Cartype = db.carTypes.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Car c, HttpPostedFileBase Picture)
        {
            if (Picture != null && Picture.ContentLength > 0)
            {
                string ext = Path.GetFileName(Picture.FileName);
                string f = Guid.NewGuid() + ext;
                Picture.SaveAs(HostingEnvironment.MapPath("~/Images/") + f);
                c.Picture = f;
            }
            if (ModelState.IsValid)
            {
                db.Cars.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cartype = db.carTypes.ToList();
            return View(c);
        }
        public ActionResult Edit(int id)
        {
            var data = db.Cars.First(x => x.CarId == id);
            ViewBag.Cartype = db.carTypes.ToList();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Car c, HttpPostedFileBase PicFile)
        {
            string f = "";
            if (PicFile != null && PicFile.ContentLength > 0)
            {
                string ext = Path.GetFileName(PicFile.FileName);
                f = Guid.NewGuid() + ext;
                PicFile.SaveAs(HostingEnvironment.MapPath("~/Images/") + f);
                c.Picture = f;
            }
            if (ModelState.IsValid)
            {
                //db.Entry(c).State = EntityState.Modified;
                Car extisting = db.Cars.First(x => x.CarId == c.CarId);
                extisting.CarId = c.CarId;
                extisting.Model = c.Model;
                extisting.Price = c.Price;
                extisting.TypeId = c.TypeId;
                if (f != "")
                    extisting.Picture = c.Picture;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cartype = db.carTypes.ToList();
            return View(c);

        }
        public ActionResult Delete(int id)
        {
            var data = db.Cars.First(x => x.CarId == id);
            return View(data);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            if (ModelState.IsValid)
            {
                var car = new Car { CarId = id };
                db.Entry(car).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(id);
        }
    }
}