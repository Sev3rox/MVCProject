using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ForumDyskusyjne.Models;

namespace ForumDyskusyjne.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: ForumCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ForumCategoryId,Name")] ForumCategory forumCategory)
        {
            if (ModelState.IsValid)
            {
                db.ForumCategorys.Add(forumCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(forumCategory);
        }

        // GET: ForumCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumCategory forumCategory = db.ForumCategorys.Find(id);
            if (forumCategory == null)
            {
                return HttpNotFound();
            }
            return View(forumCategory);
        }

        // POST: ForumCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ForumCategoryId,Name")] ForumCategory forumCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forumCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(forumCategory);
        }

        // GET: ForumCategories/Delete/5
     /*  public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumCategory forumCategory = db.ForumCategorys.Find(id);
            if (forumCategory == null)
            {
                return HttpNotFound();
            }
            return View(forumCategory);
        }

        // POST: ForumCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ForumCategory forumCategory = db.ForumCategorys.Find(id);
            db.ForumCategorys.Remove(forumCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
     */
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
