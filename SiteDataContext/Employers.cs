namespace SiteDataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employers
    {
        public Employers()
        {
            Ads = new HashSet<Ads>();
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [StringLength(15)]
        public string FullName { get; set; }

        [StringLength(30)]
        public string ContactInformation { get; set; }

        [StringLength(30)]
        public string Company { get; set; }

        public virtual ICollection<Ads> Ads { get; set; }

        public virtual Users Users { get; set; }
    }
}
