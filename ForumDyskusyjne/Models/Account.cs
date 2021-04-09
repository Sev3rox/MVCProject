using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumDyskusyjne.Models
{
    public class Account
    {
       public int AccountId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public byte[] Image { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<PrivateMessage> PrivateMessagesSend { get; set; }
        public virtual ICollection<PrivateMessage> PrivateMessagesReceived { get; set; }
        public virtual ICollection<AccountForum> AccountForums { get; set; }

    }
}