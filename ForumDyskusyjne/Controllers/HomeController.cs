using ForumDyskusyjne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ForumDyskusyjne.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.Registered = db.Users.Count();
            if (User.IsInRole("Banned")&&!User.IsInRole("Admin"))
            {

                var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
                AuthenticationManager.SignOut();
                return RedirectToAction("Ban");
            }

            return View(db.Annoucements.ToList());
        }


        public ActionResult Ban()
        {
            return View("../Banned");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}