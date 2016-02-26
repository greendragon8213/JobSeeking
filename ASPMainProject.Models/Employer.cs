using System;
using System.Collections.Generic;
using ASPMainProject.Models;

namespace ASPMainProject.Models
{
    public class Employer
    {
        public Guid Id
        {
            get;
            set;
        }

        public Guid UserId
        {
            get;
            set;
        }

        public ApprovingState ApprovingState
        {
            get;
            set;
        }

        public string Company
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }

        public string ContactInformation
        {
            get;
            set;
        }

        public List<Ad> Ads
        {
            get;
            set;
        }

    }
}
