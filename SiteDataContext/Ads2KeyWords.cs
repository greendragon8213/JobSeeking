namespace SiteDataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ads2KeyWords
    {
        public Guid Id { get; set; }

        public Guid AdId { get; set; }

        public Guid KeyWordId { get; set; }

        public virtual Ads Ads { get; set; }

        public virtual KeyWords KeyWords { get; set; }
    }
}
