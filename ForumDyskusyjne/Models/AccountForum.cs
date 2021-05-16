using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumDyskusyjne.Models
{
    public class AccountForum
    {
        public int AccountForumId {get; set;}
        public string AccountId {get; set;}
        public int ForumId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Forum Forum { get; set; }
    }
}