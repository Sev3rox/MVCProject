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

            const int PageSize = 5; // you can always do something more elegant to set this
            
            var dataSource= db.Messages.Where(a => a.ThreadId == id).ToList(); 
         

            var count = dataSource.Count();

            var data = dataSource.Skip((int)Page * PageSize).Take(PageSize).ToList();

            this.ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);

            this.ViewBag.Page = Page;

            foreach (Message msg in data)
            {
                if (db.Users.FirstOrDefault(xx => xx.Id == msg.AccountId) != null)
                    msg.User = db.Users.FirstOrDefault(xx => xx.Id == msg.AccountId);
            }

            ViewBag.Messages = data;
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // GET: Threads/Create
       
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
