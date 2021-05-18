using ForumDyskusyjne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumDyskusyjne.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminPanel
        public ActionResult Index()
        {
            var list= db.Users.ToList();
            var listpom = new List<ApplicationUser>();
            for(int i = 0; i < list.Count; i++)
            {
              
                if (list[i].Roles.Count!=0)
                {
                    listpom.Add(list[i]);
                }
            }

            foreach(var x in listpom)
            {
                if (list.Contains(x))
                {
                    list.Remove(x);
               
                }
            }

            return View(list);
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

        public ActionResult Ban(string id)
        {
            IdentityManager im = new IdentityManager();
            var user = db.Users.FirstOrDefault(a => a.Id == id);
            im.AddUserToRoleByUsername(user.UserName,"Banned");

            return RedirectToAction("Index");
        }

        public ActionResult Mod(string id)
        {
            IdentityManager im = new IdentityManager();
            var user = db.Users.FirstOrDefault(a => a.Id == id);
            im.AddUserToRoleByUsername(user.UserName, "Mod");

            return RedirectToAction("Index");
        }

        public ActionResult BlockMsg(string id)
        {
            IdentityManager im = new IdentityManager();
            var user = db.Users.FirstOrDefault(a => a.Id == id);
            im.AddUserToRoleByUsername(user.UserName, "BlockedMsg");

            return RedirectToAction("Index");
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