namespace SiteDataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Messages
    {
        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }

        public Guid ChatId { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool IsRead { get; set; }

        public virtual Chats Chats { get; set; }

        public virtual Users Users { get; set; }
    }
}
