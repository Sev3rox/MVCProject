using ForumDyskusyjne.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumDyskusyjne.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {

        public static string idd;

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminPanel
        public ActionResult Index()
        {
            var list= db.Users.ToList();
            var listpom = new List<ApplicationUser>();
            for(int i = 0; i < list.Count; i++)
            {
               
                if ((list[i].Roles).FirstOrDefault(a=>a.RoleId==(db.Roles.FirstOrDefault(b=>b.Name=="Admin").Id))!=null)
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
           
           
           
            var Ban = (db.Roles.FirstOrDefault(b => b.Name == "Banned").Id);
            ViewBag.Banned = Ban;
            var Mod = (db.Roles.FirstOrDefault(b => b.Name == "Mod").Id);
            ViewBag.Mod = Mod;
            var Block = (db.Roles.FirstOrDefault(b => b.Name == "BlockedMsg").Id);
            ViewBag.BlockedMsg = Block;


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

        public ActionResult UnBan(string id)
        {
            IdentityManager im = new IdentityManager();
            var user = db.Users.FirstOrDefault(a => a.Id == id);
            im.UserRoleRemove(user.Id, "Banned");

            return RedirectToAction("Index");
        }

        public ActionResult Mod(string id)
        {
            IdentityManager im = new IdentityManager();
            var user = db.Users.FirstOrDefault(a => a.Id == id);
            im.AddUserToRoleByUsername(user.UserName, "Mod");

            return RedirectToAction("Index");
        }

        public ActionResult UnMod(string id)
        {
            IdentityManager im = new IdentityManager();
            var user = db.Users.FirstOrDefault(a => a.Id == id);
            im.UserRoleRemove(user.Id, "Mod");

            return RedirectToAction("Index");
        }

        public ActionResult BlockMsg(string id)
        {
            IdentityManager im = new IdentityManager();
            var user = db.Users.FirstOrDefault(a => a.Id == id);
            im.AddUserToRoleByUsername(user.UserName, "BlockedMsg");

            return RedirectToAction("Index");
        }

        public ActionResult UnBlockMsg(string id)
        {
            IdentityManager im = new IdentityManager();
            var user = db.Users.FirstOrDefault(a => a.Id == id);
            im.UserRoleRemove(user.Id, "BlockedMsg");

            return RedirectToAction("Index");
        }

        public int returnimg(int x)
        {
            if (x >= 1000)
                return 1000;
            if (x >= 500)
                return 500;
            if (x >= 100)
                return 100;
            if (x >= 50)
                return 50;
            if (x >= 10)
                return 10;
            if (x >= 5)
                return 5;
            if (x >= 1)
                return 1;

            return 0;


        }

        public ActionResult Ranks(string id)
        {
            idd = id;
            var us = db.Users.Find(id);
            ViewBag.User = us;
            var x = returnimg(us.msg);
            ViewBag.idd = id;
            ViewBag.Image = db.Ranks.FirstOrDefault(a => a.Name == x.ToString()).Image;
            return View();
        }

        [HttpPost]
        public ActionResult Ranks(string id, string submitButton)
        {
            var us = db.Users.Find(idd);
            if (submitButton == "+10")
            {
                us.msg = us.msg + 10;
            }
            else
            {
                us.msg = us.msg - 10;
                if (us.msg < 0)
                {
                    us.msg = 0;
                }
            }
            db.Entry(us).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Ranks", new { id = idd });
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