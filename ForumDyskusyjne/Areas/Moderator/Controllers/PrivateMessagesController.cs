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

namespace ForumDyskusyjne.Areas.Moderator.Controllers
{
    [Authorize(Roles = "Mod")]
    public class PrivateMessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PrivateMessages
        public ActionResult Index()
        {
            List<KeyValuePair<string,string>> pairs = new List<KeyValuePair<string, string>>();
            foreach(var r in db.Users)
            {
                pairs.Add(new KeyValuePair<string, string>(r.Id, r.UserName));
            }
            ViewBag.pairs = pairs;
            var idd = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(a => a.Id == idd);
            var tempo = db.AccountForums.Where(b => b.AccountId == idd).Select(a=>a.Forum);
            var tempoo = tempo.Select(a=>a.ForumId).ToList();
            ICollection<string> tempooo = new List<string>();
            foreach(int f in tempoo)
            {
                tempooo.Add(toto(f));
            }
            var tempa = tempo.Select(a => a.Threads);
            ICollection<string> newtempa = new List<string>();
            foreach(ICollection<Thread> ic in tempa)
            {
                foreach(Thread t in ic)
                {
                    newtempa.Add(toto(t.ThreadId));
                }
            }
            return View(db.PrivateMessages.Where(a=>a.ReceiverId==idd||a.SenderId==idd||(a.ReceiverId==null&&a.Text.Contains("<div")&&tempooo.Contains(a.Text.Substring(51,3)))).ToList());
        }
        public string toto(int nu)
        {
            char[] n = { ' ', ' ', ' ' };
            string c = nu.ToString();
            for (int i = 0; i < c.Length; i++)
            {
                n[i] = c[i];
            }
            return new string(n);
        }

        // GET: PrivateMessages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivateMessage privateMessage = db.PrivateMessages.Find(id);
            if (privateMessage == null)
            {
                return HttpNotFound();
            }
            return View(privateMessage);
        }

        // GET: PrivateMessages/Create
        public ActionResult Create()
        {
            ViewBag.UserName = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: PrivateMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrivateMessageId,Text")] PrivateMessage privateMessage,string UserName)
        {
            if (ModelState.IsValid)
            {
                var idd = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(a => a.Id == idd);
                privateMessage.ReceiverId = UserName;
                privateMessage.SenderId = user.Id;
                db.PrivateMessages.Add(privateMessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(privateMessage);
        }

        // GET: PrivateMessages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivateMessage privateMessage = db.PrivateMessages.Find(id);
            if (privateMessage == null)
            {
                return HttpNotFound();
            }
            return View(privateMessage);
        }

        // POST: PrivateMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrivateMessageId,Text,SenderId,ReceiverId,Attachment")] PrivateMessage privateMessage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(privateMessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(privateMessage);
        }

        // GET: PrivateMessages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivateMessage privateMessage = db.PrivateMessages.Find(id);
            if (privateMessage == null)
            {
                return HttpNotFound();
            }
            return View(privateMessage);
        }

        // POST: PrivateMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrivateMessage privateMessage = db.PrivateMessages.Find(id);
            db.PrivateMessages.Remove(privateMessage);
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
