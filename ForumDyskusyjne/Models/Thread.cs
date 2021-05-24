using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumDyskusyjne.Models
{
    public class Thread
    {
        public int ThreadId { get; set; }
        [Required]
        public string Name { get; set; }
        public int Order { get; set; }
        public int Views { get; set; }
        public int Glued { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public int ForumId { get; set; }
        public virtual Forum Forum { get; set; }

    }
}