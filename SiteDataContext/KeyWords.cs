namespace SiteDataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KeyWords
    {
        public KeyWords()
        {
            Ads2KeyWords = new HashSet<Ads2KeyWords>();
            Candidates2KeyWords = new HashSet<Candidates2KeyWords>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Value { get; set; }

        public virtual ICollection<Ads2KeyWords> Ads2KeyWords { get; set; }

        public virtual ICollection<Candidates2KeyWords> Candidates2KeyWords { get; set; }
    }
}
