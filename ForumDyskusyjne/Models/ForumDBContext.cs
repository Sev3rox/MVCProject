using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ForumDyskusyjne.Models
{
    public class ForumDBContext :DbContext
    {
        public ForumDBContext() : base("ForumCS") { }
        public DbSet<Example> Examples { get; set; }
    }
}