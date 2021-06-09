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

namespace ForumDyskusyjne.Controllers
{
    public class MessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Messages
       

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create(int id)
        {
            Message msg = new Message();
            msg.AccountId = User.Identity.GetUserId();


            int max = 0;
            if (db.Messages.ToList().Count != 0)
                 max = db.Messages.Max(a => a.Order);
            msg.Order = max + 1;
            msg.ThreadId=id;
         
            return View(msg);
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MessageId,Content,Order,ThreadId,AccountId")] Message message)
        {
            foreach (BannedWord w in db.BannedWords)
            {
                if (message.Content.ToLower().Contains(w.Word.ToLower())) ModelState.AddModelError("", "Użyto zakazane słowo " + w.Word);
            }
            if (ModelState.IsValid)
            {

                db.Messages.Add(message);


                var idd = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(a => a.Id == idd);
                user.msg = user.msg + 1;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                db.SaveChanges();
                return Redirect("/Threads/Details/" + message.ThreadId + "?Page=0");

            }

            ViewBag.ThreadId = new SelectList(db.Threads, "ThreadId", "Name", message.ThreadId);
            return View(message);
        }

        // GET: Messages/Edit/5
        

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
