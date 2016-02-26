using System.Collections.Generic;

namespace ASPMainProject.Models
{
    public class CandidateVM
    {
        public List<CandidatePreview> CandidatePreviews
        {
            get;
            set;
        }

        public List<KeyWordPreview> KeyWordPreviews
        {
            get;
            set;
        }
    }
}
