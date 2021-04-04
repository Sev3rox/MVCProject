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
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Annoucement> Annoucements { get; set; }
        public DbSet<BannedWord> BannedWords { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<ForumCategory> ForumCategorys { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<Thread> Threads { get; set; }
    }
}