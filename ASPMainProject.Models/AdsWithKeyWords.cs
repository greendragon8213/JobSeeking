using System.Collections.Generic;
using ASPMainProject.Models;

namespace ASPMainProject.Models
{
    public class AdsWithKeyWords
    {
        public List<Ad> Ads
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
