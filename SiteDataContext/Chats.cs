namespace SiteDataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Chats
    {
        public Chats()
        {
            Messages = new HashSet<Messages>();
        }

        public Guid Id { get; set; }

        public Guid UserId1 { get; set; }

        public Guid UserId2 { get; set; }

        public virtual Users Users { get; set; }

        public virtual Users Users1 { get; set; }

        public virtual ICollection<Messages> Messages { get; set; }
    }
}
