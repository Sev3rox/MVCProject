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
    public class ThreadsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Threads
        public ActionResult Index(int? id)
        {
            var threads = db.Threads.Where(a=>a.ForumId==id);
            ViewBag.ForumId = id;
            return View(threads.ToList());
        }

        // GET: Threads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            ViewBag.Messages = db.Messages.Where(a => a.ThreadId==id).ToList();
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // GET: Threads/Create
        public ActionResult Create(int id)
        {
            ViewBag.ForumId = new SelectList(db.Forums, "ForumId", "Name");

            Thread thr = new Thread();

            int max = db.Threads.Max(a => a.Order);
            thr.Order = max + 1;
            thr.ForumId = id;
            thr.Views = 0;
            thr.Glued = 0;
            return View(thr);

        }
        // POST: Threads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ThreadId,Name,Order,Views,Glued,ForumId")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                db.Threads.Add(thread);
                db.SaveChanges();
                return Redirect("/Threads/Index/" + thread.ForumId);
            }

            ViewBag.ForumId = new SelectList(db.Forums, "ForumId", "Name", thread.ForumId);
            return View(thread);
        }

        // GET: Threads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            ViewBag.ForumId = new SelectList(db.Forums, "ForumId", "Name", thread.ForumId);
            return View(thread);
        }

        // POST: Threads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ThreadId,Name,Order,Views,Glued,ForumId")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thread).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ForumId = new SelectList(db.Forums, "ForumId", "Name", thread.ForumId);
            return View(thread);
        }

        // GET: Threads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // POST: Threads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thread thread = db.Threads.Find(id);
            db.Threads.Remove(thread);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
