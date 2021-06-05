using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ForumDyskusyjne.Models;
using Microsoft.AspNet.Identity;

namespace ForumDyskusyjne.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MessagesController : Controller
    {

        static int pomid;
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Messages
        public ActionResult Index()
        {
            var messages = db.Messages.Include(m => m.Thread);
            return View(messages.ToList());
        }

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
            foreach (BannedWord w in db.BannedWords.Where(a=>!a.Word.StartsWith("<")))
            {
                if (message.Content.ToLower().Contains(w.Word.ToLower())) ModelState.AddModelError("","Użyto zakazane słowo "+w.Word);
            }
            if (ModelState.IsValid)
            {
                foreach (BannedWord w in db.BannedWords.Where(a => a.Word.StartsWith("<")))
                {
                    while(message.Content.Contains(w.Word.Substring(0,2)))
                    {
                        int temp = message.Content.IndexOf(w.Word.Substring(0, 2));
                        for(int i=temp;;i++)
                        {
                            if(message.Content[i]=='>')
                            {
                                if (message.Content[i - 1] == '/')
                                {
                                    string a = message.Content.Substring(0, temp);
                                    string b = message.Content.Substring(i+1, message.Content.Length - (i+1));
                                    message.Content =a + b;
                                }
                                else
                                {
                                    string a = message.Content.Substring(0, temp);
                                    string b3 = "</" + w.Word.Substring(1, w.Word.Length - 2) + ">";
                                    int b1 = message.Content.IndexOf(b3);
                                    int b4 = b1 + w.Word.Length + 1;
                                    int b2 = message.Content.Length -b4;
                                    string b = message.Content.Substring(b1, b2);
                                    message.Content = a + b;
                                }
                                break;
                            }
                        }
                    }
                }
                db.Messages.Add(message);
                var idd = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(a => a.Id == idd);
                user.msg = user.msg + 1;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                
                return Redirect("/Admin/Threads/Details/" + message.ThreadId + "?Page=0");
                
            }

            ViewBag.ThreadId = new SelectList(db.Threads, "ThreadId", "Name", message.ThreadId);
            return View(message);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id, int id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id2 != 0)
            {
                Message message = db.Messages.Find(id);
                if (message == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ThreadId = new SelectList(db.Threads, "ThreadId", "Name", message.ThreadId);
                pomid = id2;
                return View(message);
            }
            else
            {
                Message message = db.Messages.Find(id);
                if (message == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ThreadId = new SelectList(db.Threads, "ThreadId", "Name", message.ThreadId);
                return View(message);
            }
        }
        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MessageId,Content,Order,ThreadId,AccountId")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
           
                return RedirectToAction("../Threads/Details", new { id = pomid, Page = 0 });
            }
            ViewBag.ThreadId = new SelectList(db.Threads, "ThreadId", "Name", message.ThreadId);
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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
