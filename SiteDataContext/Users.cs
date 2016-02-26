namespace SiteDataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        public Users()
        {
            Candidates = new HashSet<Candidates>();
            Chats = new HashSet<Chats>();
            Chats1 = new HashSet<Chats>();
            Employers = new HashSet<Employers>();
            Messages = new HashSet<Messages>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(64)]
        public string UserName { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }

        public int Role { get; set; }

        public int Position { get; set; }

        public int ApprovingStatus { get; set; }

        public virtual ICollection<Candidates> Candidates { get; set; }

        public virtual ICollection<Chats> Chats { get; set; }

        public virtual ICollection<Chats> Chats1 { get; set; }

        public virtual ICollection<Employers> Employers { get; set; }

        public virtual ICollection<Messages> Messages { get; set; }
    }
}
