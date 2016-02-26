using System;
using System.Collections.Generic;

namespace ASPMainProject.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public Guid InterlocutorId { get; set; }
        public Guid CurrentUserId { get; set; }
        public string InterlocutorName { get; set; }
        public List<Message> Messages { get; set; }
    }
}
