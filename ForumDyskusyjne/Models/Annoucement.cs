using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumDyskusyjne.Models
{
    public class Annoucement
    {
        public int AnnoucementId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}