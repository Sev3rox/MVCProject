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
    public class AnnoucementsController : Controller
    {
        private ForumDBContext db = new ForumDBContext();

        // GET: Annoucements
        public ActionResult Index()
        {
            return View(db.Annoucements.ToList());
        }

        // GET: Annoucements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annoucement annoucement = db.Annoucements.Find(id);
            if (annoucement == null)
            {
                return HttpNotFound();
            }
            return View(annoucement);
        }

        // GET: Annoucements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Annoucements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnnoucementId,Text")] Annoucement annoucement)
        {
            if (ModelState.IsValid)
            {
                db.Annoucements.Add(annoucement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(annoucement);
        }

        // GET: Annoucements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annoucement annoucement = db.Annoucements.Find(id);
            if (annoucement == null)
            {
                return HttpNotFound();
            }
            return View(annoucement);
        }

        // POST: Annoucements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnnoucementId,Text")] Annoucement annoucement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(annoucement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(annoucement);
        }

        // GET: Annoucements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annoucement annoucement = db.Annoucements.Find(id);
            if (annoucement == null)
            {
                return HttpNotFound();
            }
            return View(annoucement);
        }

        // POST: Annoucements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Annoucement annoucement = db.Annoucements.Find(id);
            db.Annoucements.Remove(annoucement);
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
