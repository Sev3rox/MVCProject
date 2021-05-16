using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumDyskusyjne.Models
{
    public class PrivateMessage
    {
    
        public int PrivateMessageId { get; set; }
        public string Text { get; set; }
        public string SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        public string ReceiverId { get; set; }
        public virtual ApplicationUser Receiver { get; set; }
        public byte[] Attachment { get; set; }
    }
}
