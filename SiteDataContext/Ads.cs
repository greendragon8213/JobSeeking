namespace SiteDataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ads
    {
        public Ads()
        {
            Ads2KeyWords = new HashSet<Ads2KeyWords>();
        }

        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        [StringLength(30)]
        public string Company { get; set; }

        public int ActualState { get; set; }

        [StringLength(30)]
        public string Location { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        public int? SalaryPerMonth { get; set; }

        public virtual Employers Employers { get; set; }

        public virtual ICollection<Ads2KeyWords> Ads2KeyWords { get; set; }
    }
}
