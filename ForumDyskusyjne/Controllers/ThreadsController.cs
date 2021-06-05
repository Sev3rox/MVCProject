using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
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
            var threadss = db.Threads.Where(a => a.ForumId == id).ToList();
            ViewBag.Threads = threadss.Count();
            ViewBag.Messages = 0;
            foreach (Thread t in threadss)
            {
                ViewBag.Messages += db.Messages.Where(a => a.ThreadId == t.ThreadId).Count();
            }
            Forum forum = db.Forums.Find(id);
            if (forum.Permission == 1)
            {
                return RedirectToAction("IndexA", new { id = id });
            }
            var threads = db.Threads.Where(a => a.ForumId == id).OrderByDescending(a=>a.Glued).ThenBy(a=>a.Order);
            ViewBag.ForumId = id;
           
            return View(threads.ToList());
        }


        [Authorize]
        public ActionResult IndexA(int? id)
        {
            var threadss = db.Threads.Where(a => a.ForumId == id).ToList();
            ViewBag.Threads = threadss.Count();
            ViewBag.Messages = 0;
            foreach (Thread t in threadss)
            {
                ViewBag.Messages += db.Messages.Where(a => a.ThreadId == t.ThreadId).Count();
            }
            var threads = db.Threads.Where(a => a.ForumId == id).OrderByDescending(a => a.Glued).ThenBy(a => a.Order);
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
            int g = db.Messages.Where(a => a.ThreadId == id).Count();
            ViewBag.Messagess = g;
            ViewBag.Views = db.Threads.Find(id).Views;


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
            IQueryable<Message>[] pom;
            if (!Request.QueryString["Search"].IsEmpty())
            {
                ViewBag.test = Request.QueryString["Search"];

                var temp = Request.QueryString["Search"].Split((char)10);
                pom = new IQueryable<Message>[temp.Length];
                for (int i = 0; i < temp.Length; i++)
                {
                    string[] h = temp[i].Split((char)13);
                    temp[i] = h[0];
                }
                int x = 5;
                for (int i = 0; i < temp.Length; i++)
                {
                    string s = temp[i];
                    var c = s.Split('\"');
                    if (s.ToLower().Contains("\"or\""))
                    {
                        int l1 = Int32.Parse(c[0]);
                        int l2 = Int32.Parse(c[2]);
                        pom[i] = pom[l1].Union(pom[l2]);
                    }
                    else
                    {
                        if (s.ToLower().Contains("\"and\""))
                        {
                            int l1 = Int32.Parse(c[0]);
                            int l2 = Int32.Parse(c[2]);
                            pom[i] = pom[l1].Intersect(pom[l2]);
                        }
                        else
                        {
                            if (s.ToLower().Contains("\"not\""))
                            {
                                int l2 = Int32.Parse(c[2]);
                                pom[i] = db.Messages.Except(pom[l2]);
                            }
                            else
                            {
                                pom[i] = db.Messages.Where(a => a.Content.ToLower().Contains(s));
                            }
                        }
                    }
                }
                dataSource = pom[pom.Length - 1].ToList();
            }

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
