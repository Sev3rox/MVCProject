using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumDyskusyjne.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        [Required]
        public string Content { get; set; }
        public int Order { get; set; }
        public int ThreadId { get; set; }
        public virtual Thread Thread{ get; set; }
        public string AccountId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}