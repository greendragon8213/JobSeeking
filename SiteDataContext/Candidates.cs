namespace SiteDataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Candidates
    {
        public Candidates()
        {
            Candidates2KeyWords = new HashSet<Candidates2KeyWords>();
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [StringLength(50)]
        public string ExpectedPosition { get; set; }

        [StringLength(30)]
        public string Location { get; set; }

        public int? SalaryPerMonth { get; set; }

        public int? ExperienceYears { get; set; }

        public string ExperienceDescription { get; set; }

        public string Skills { get; set; }

        public virtual ICollection<Candidates2KeyWords> Candidates2KeyWords { get; set; }

        public virtual Users Users { get; set; }
    }
}
