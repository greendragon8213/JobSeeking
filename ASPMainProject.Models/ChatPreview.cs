using System;

namespace ASPMainProject.Models
{
    public class ChatPreview
    {
        public Guid Id { get; set; }
        public int  NewMessagesCount { get; set; }
        public DateTime LastMessageDate { get; set; }
        public string LastMessageContent { get; set; }
        public string InterlocutorName { get; set; }
    }
}
