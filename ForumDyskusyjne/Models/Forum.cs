using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumDyskusyjne.Models
{
    public class Forum
    {
        public int ForumId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Permission{ get; set; }
        public int ForumCategoryId { get; set; }
        public virtual ForumCategory ForumCategory { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }
    }
}