using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ForumDyskusyjne.Models;

namespace ForumDyskusyjne.Controllers
{
    public class ForumCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ForumCategories
        public ActionResult Index()
        {
            var list = db.ForumCategorys.ToList();
            var x = db.ForumCategorys.FirstOrDefault(a=>a.Name=="no Category");
            if (x != null)
            {
                if (list.Contains(x))
                    list.Remove(x);
            }

            return View(list);
        }

        // GET: ForumCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumCategory forumCategory = db.ForumCategorys.Find(id);
            ViewBag.Forums = db.Forums.Where(a => a.ForumCategoryId == id).ToList();
            if (forumCategory == null)
            {
                return HttpNotFound();
            }
            return View(forumCategory);
        }


          

     
            

        // GET: ForumCategories/Create
      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
