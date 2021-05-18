using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ForumDyskusyjne.Models;
using Microsoft.AspNet.Identity;

namespace ForumDyskusyjne.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ThreadsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Threads
        public ActionResult Index(int? id)
        {
            var xd = id;
            Forum forum = db.Forums.Find(id);
            if (forum.Permission == 1)
            {
                return RedirectToAction("IndexA", id);
            }
            var threads = db.Threads.Where(a => a.ForumId == id).OrderByDescending(a=>a.Glued).ThenBy(a=>a.Order);
            ViewBag.ForumId = id;
           
            return View(threads.ToList());
        }


        [Authorize]
        public ActionResult IndexA(int? id)
        {
           
            var threads = db.Threads.Where(a => a.ForumId == id);
            ViewBag.ForumId = id;

            return View("Index",threads.ToList());
        }



        public ActionResult Pin(int? id)
        {
            var thread = db.Threads.Find(id);
            thread.Glued = 1;
            int fid = thread.ForumId;
            db.Entry(thread).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index",new { id = fid});
        }

        public ActionResult UnPin(int? id)
        {
            var thread = db.Threads.Find(id);
            thread.Glued = 0;
            int fid = thread.ForumId;
            db.Entry(thread).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = fid });
        }


        // GET: Threads/Details/5
        public ActionResult Details(int? id, int Page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);



            //var allthreads = db.Threads.Where(a => a.ForumId == id).OrderByDescending(a => a.Glued).ThenBy(a => a.Order).ToList();
            //ViewBag.ForumId = id;
            //var threads = new List<Thread>();
            //for (int i = 0; i < 10; i++)
            //{
            //    if (i < allthreads.Count)
            //        threads.Add(allthreads[i]);
            //}

            int PageSize = 5; 
            IdentityManager im = new IdentityManager();
            var user = im.GetUserByID(User.Identity.GetUserId());
            if (user.onpage != 0)
            {
                PageSize = user.onpage;
            }



            var dataSource= db.Messages.Where(a => a.ThreadId == id).ToList(); 
            

            var count = dataSource.Count();

            var data = dataSource.Skip((int)Page * PageSize).Take(PageSize).ToList();

            this.ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);

            this.ViewBag.Page = Page;
            foreach (Message msg in data)
            {
                if (db.Users.FirstOrDefault(x => x.Id == msg.AccountId) != null)
                    msg.User = db.Users.FirstOrDefault(x => x.Id == msg.AccountId);
            }
            ViewBag.Messages = data;
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
            int max = 0;
            if (db.Threads.ToList().Count!=0)
            max = db.Threads.Max(a => a.Order);
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
                return Redirect("/Admin/Threads/Index/" + thread.ForumId);
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
                return RedirectToAction("/Index/" + thread.ForumId);
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


        public ActionResult DeleteMsg(int id, int id2)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = id2, Page = 0 });
        }
        // POST: Threads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thread thread = db.Threads.Find(id);
            var idd = thread.ForumId;
            db.Threads.Remove(thread);
            db.SaveChanges();
            return RedirectToAction("/Index/" + idd);
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
