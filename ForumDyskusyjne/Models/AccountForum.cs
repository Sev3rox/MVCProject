using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumDyskusyjne.Models
{
    public class AccountForum
    {
        public int AccountForumId {get; set;}
        public int AccountId {get; set;}
        public int ForumId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Forum Forum { get; set; }
    }
}