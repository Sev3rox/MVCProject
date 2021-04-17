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
    public class BannedWordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BannedWords
        public ActionResult Index()
        {
            return View(db.BannedWords.ToList());
        }

        // GET: BannedWords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BannedWord bannedWord = db.BannedWords.Find(id);
            if (bannedWord == null)
            {
                return HttpNotFound();
            }
            return View(bannedWord);
        }

        // GET: BannedWords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BannedWords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BannedWordId,Word")] BannedWord bannedWord)
        {
            if (ModelState.IsValid)
            {
                db.BannedWords.Add(bannedWord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bannedWord);
        }

        // GET: BannedWords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BannedWord bannedWord = db.BannedWords.Find(id);
            if (bannedWord == null)
            {
                return HttpNotFound();
            }
            return View(bannedWord);
        }

        // POST: BannedWords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BannedWordId,Word")] BannedWord bannedWord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bannedWord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bannedWord);
        }

        // GET: BannedWords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BannedWord bannedWord = db.BannedWords.Find(id);
            if (bannedWord == null)
            {
                return HttpNotFound();
            }
            return View(bannedWord);
        }

        // POST: BannedWords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BannedWord bannedWord = db.BannedWords.Find(id);
            db.BannedWords.Remove(bannedWord);
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
