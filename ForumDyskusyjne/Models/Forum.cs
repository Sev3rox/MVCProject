using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumDyskusyjne.Models
{
    public class Forum
    {
        public int ForumId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Permission{ get; set; }
        public int ForumCategoryId { get; set; }
        public virtual ForumCategory ForumCategory { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }
        public virtual ICollection<AccountForum> AccountForums { get; set; }
    }
}