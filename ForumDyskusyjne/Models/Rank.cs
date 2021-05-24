using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumDyskusyjne.Models
{
    public class Rank
    {
        public int RankId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}