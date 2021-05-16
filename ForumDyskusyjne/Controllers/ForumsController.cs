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
    public class ForumsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Forums
        public ActionResult Index()
        {
            //IdentityManager im = new IdentityManager();
            //im.CreateRole("Admin");
            //im.CreateRole("User");
            //im.CreateRole("Mod");
            //im.CreateRole("Banned");
            //im.CreateRole("BlockedMsg");
      

            var forums = db.Forums.Include(f => f.ForumCategory);
            return View(forums.ToList());
        }

        public ActionResult Mods(int id)
        {
            string mods = "";
            var added = db.Users.Where(a => a.AccountForums.Any(b => b.ForumId == id)).ToList();
            var all = db.Users.ToList();
          foreach(var x in all)
            {
                x.AccountForums.Add(db.AccountForums.FirstOrDefault(a => a.AccountId == x.Id));
            }

            foreach (var x in all)
            {
                foreach (var y in x.AccountForums)
                {
                    if(y!=null)
                    if (y.ForumId == id)
                        added.Add(db.Users.FirstOrDefault(a=>a.Id==y.AccountId));
            }
            }


            foreach (ApplicationUser x in added)
            {
                if (all.Contains(x))
                {
                    all.Remove(x);
                    mods += x.UserName + ", ";
                }
            }
            ViewBag.FId = id;
            ViewBag.mods = mods;
            ViewBag.ForumModsAdd = new SelectList(all, "UserName", "UserName");
            ViewBag.ForumModsDel = new SelectList(added, "UserName", "UserName");
            return View();
        }


        [HttpPost]
        public ActionResult Mods(int ForumId, string btn, string Account)
        {
            var idd = db.Users.FirstOrDefault(a => a.UserName == Account);
            var id = idd.Id;
            switch (btn)
            {

                case "Add":
                    {
                       
                        var fa = new AccountForum
                        {
                            AccountId = id,
                            ForumId = ForumId
                        };



                        db.AccountForums.Add(fa);
                        db.SaveChanges();
                        break;
                    }
                case "Del":
                    {

                        var fa = db.AccountForums.FirstOrDefault(a => a.ForumId == ForumId && a.AccountId == id);
                             db.AccountForums.Remove(fa);
                        db.SaveChanges();
                        break;
                    }
              
            }
                    return RedirectToAction("Mods", new { id = ForumId });
        }
        // GET: Forums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Forums.Find(id);

         

            ViewBag.Threads = db.Threads.Where(a => a.ForumId == id).ToList();
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }


        // GET: Forums/Create
        public ActionResult Create()
        {
            ViewBag.ForumCategoryId = new SelectList(db.ForumCategorys, "ForumCategoryId", "Name");
            ViewBag.Permission = new SelectList(
    new List<SelectListItem>
    {
        new SelectListItem { Text = "Blocked for notlogged", Value = "1"},
        new SelectListItem { Text = "Open for notlogged", Value = "2"},
    }, "Value", "Text");
            return View();
        }

        // POST: Forums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ForumId,Name,Description,Permission,ForumCategoryId")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                
                db.Forums.Add(forum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ForumCategoryId = new SelectList(db.ForumCategorys, "ForumCategoryId", "Name", forum.ForumCategoryId);
            return View(forum);
        }

        // GET: Forums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Forums.Find(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            ViewBag.ForumCategoryId = new SelectList(db.ForumCategorys, "ForumCategoryId", "Name", forum.ForumCategoryId);
            return View(forum);
        }

        // POST: Forums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ForumId,Name,Description,Permission,ForumCategoryId")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ForumCategoryId = new SelectList(db.ForumCategorys, "ForumCategoryId", "Name", forum.ForumCategoryId);
            return View(forum);
        }

        // GET: Forums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Forums.Find(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // POST: Forums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Forum forum = db.Forums.Find(id);
            db.Forums.Remove(forum);
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
