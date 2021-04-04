using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumDyskusyjne.Models
{
    public class ForumCategory
    {
        public int ForumCategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Forum> Forums { get; set; }
    }
}