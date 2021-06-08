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
using Microsoft.AspNet.Identity;

namespace ForumDyskusyjne.Areas.User.Controllers
{
    [Authorize]
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
        public ActionResult Details(int? id, int Page,string Search)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Thread thread = db.Threads.Find(id);
            int g = db.Messages.Where(a => a.ThreadId == id).Count();
            ViewBag.Messagess = g;
            ViewBag.Views = db.Threads.Find(id).Views;
            var userid=User.Identity.GetUserId();
            var x = db.AccountForums.FirstOrDefault(a => a.AccountId == userid && a.ForumId== thread.ForumId);
            if (User.IsInRole("Mod")&&x!=null)
            {
                return RedirectToAction("Details", new { id = id, Page = 0, area = "Moderator" });
            }

            if (User.IsInRole("BlockedMsg"))
            {
                return RedirectToAction("Details", new { id = id, Page = 0, area = "BlockedMsg" });
            }

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
            if (user.onpage > 0)
            {
                PageSize = user.onpage;
            }

            var dataSource = db.Messages.Where(a => a.ThreadId == id).ToList();
            IQueryable<Message>[] pom;
            string ten = Request.QueryString["Search"];
            if (ten.IsEmpty() && ten == "")
                ten = Search;
            if (!ten.IsEmpty() && ten != "")
            {
                ViewBag.test = ten;
                int xxx = 4;
                var temp = ten.Split((char)10);
                pom = new IQueryable<Message>[temp.Length];
                for (int i = 0; i < temp.Length; i++)
                {
                    string[] h = temp[i].Split((char)13);
                    temp[i] = h[0];
                }
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
            ViewBag.SString = ten;

            var count = dataSource.Count();

            var data = dataSource.Skip((int)Page * PageSize).Take(PageSize).ToList();
            thread.Views++;
            db.Entry(thread).State = EntityState.Modified;
            db.SaveChanges();
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
        public ActionResult Report(int? id, int id2)
        {
            PrivateMessage pm = new PrivateMessage();
            pm.ReceiverId = null;
            pm.SenderId = User.Identity.GetUserId();
            int temp = db.Forums.First(a => a.Threads.Any(b=>b.ThreadId==id2)).ForumId;
            pm.Text = "<div style=\"color:red\">Reported message: Forum Id: "+ toto(temp) + " Thread Id: " + id2.ToString() + " Message Id: " + id.ToString() + "</div>";
            string g = pm.Text.Substring(51, 3);
            db.PrivateMessages.Add(pm);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = id2, Page = 0 });
        }
        public string toto(int nu)
        {
            char[] n = { ' ',' ',' '};
            string c = nu.ToString();
            for(int i=0;i<c.Length;i++)
            {
                n[i] = c[i];
            }
            return new string(n);
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
                return Redirect("/User/Threads/Index/" + thread.ForumId);
            }

            ViewBag.ForumId = new SelectList(db.Forums, "ForumId", "Name", thread.ForumId);
            return View(thread);
        }

        // GET: Threads/Edit/5
        
        

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
