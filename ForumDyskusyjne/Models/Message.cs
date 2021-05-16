using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumDyskusyjne.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public int ThreadId { get; set; }
        public virtual Thread Thread{ get; set; }
        public string AccountId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}