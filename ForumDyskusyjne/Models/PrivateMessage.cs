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
        public int SenderId { get; set; }
        public virtual Account Sender { get; set; }
        public int ReceiverId { get; set; }
        public virtual Account Receiver { get; set; }
        public byte[] Attachment { get; set; }
    }
}
