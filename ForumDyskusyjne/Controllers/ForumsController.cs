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
            //im.CreateRole("Mod");
            //im.CreateRole("Banned");
            //im.CreateRole("BlockedMsg");
            //im.AddUserToRoleByUsername("user1@gmail.com", "Admin");

            var forums = db.Forums.Include(f => f.ForumCategory);
            return View(forums.ToList());
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
