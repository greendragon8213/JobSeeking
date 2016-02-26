namespace SiteDataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Candidates2KeyWords
    {
        public Guid Id { get; set; }

        public Guid CandidateId { get; set; }

        public Guid KeyWordsId { get; set; }

        public virtual Candidates Candidates { get; set; }

        public virtual KeyWords KeyWords { get; set; }
    }
}
