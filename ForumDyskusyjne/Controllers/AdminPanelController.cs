using ForumDyskusyjne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumDyskusyjne.Controllers
{
    public class AdminPanelController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminPanel
        public ActionResult Index()
        {
            
            return View(db.Users.ToList());
        }

        public ActionResult Comments()
        {
            var pom = db.Messages.ToList();
            foreach(Message msg in pom)
            {
                if(db.Users.FirstOrDefault(x => x.Id == msg.AccountId)!=null)
                msg.User= db.Users.FirstOrDefault(x => x.Id == msg.AccountId);
            }
            return View(pom);
        }

        public ActionResult Delete(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Comments");
        }
    }
}