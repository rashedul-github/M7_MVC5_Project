using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MyMVCProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        CarDbContext db = new CarDbContext(); 
        public ActionResult Index()
        {
            return View(db.carTypes.ToList());
        }
        public PartialViewResult Carget(int id)
        {
            var data = db.Cars.Include(m => m.CarType).Where(x => x.TypeId == id);
            ViewBag.TypeName = db.carTypes.First(x => x.TypeId == id).TypeName;
            return PartialView("_Carget", data.ToList());
        }
        public ActionResult GetSearch(string sortOrder, string sortBy)
        {
            ViewBag.sortOrder = sortOrder;
            var users = db.Cars.ToList();
            switch (sortBy)
            {
                case "CarId":
                    {
                        switch (sortOrder)
                        {
                            case "Asc":
                                {
                                    users = users.OrderBy(x => x.CarId).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    users = users.OrderByDescending(x => x.CarId).ToList();
                                    break;
                                }
                            default:
                                {
                                    users = users.OrderBy(x => x.CarId).ToList();
                                    break;
                                }
                        }
                        break;
                    }

                case "Model":
                    {
                        switch (sortOrder)
                        {
                            case "Asc":
                                {
                                    users = users.OrderBy(x => x.Model).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    users = users.OrderByDescending(x => x.Model).ToList();
                                    break;
                                }
                            default:
                                {
                                    users = users.OrderBy(x => x.Model).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        users = users.OrderBy(x => x.CarId).ToList();
                        break;
                    }
            }
            return View(users);
        }
        [HttpPost]
        public ActionResult GetSearch(string searching)
        {
            var users = db.Cars.ToList();
            if (searching != null)
            {
                users = db.Cars.Where(x => x.Model.Contains(searching)).ToList();
            }

            return View(users);
        }
    }
}