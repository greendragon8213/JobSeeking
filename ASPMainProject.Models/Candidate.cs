using System;
using System.Collections.Generic;

namespace ASPMainProject.Models
{
    public enum ApprovingState { UnApproved, Approved };
    public class Candidate
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

        public string ExpectedPosition
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }

        public int SalaryPerMonth
        {
            get;
            set;
        }

        public int ExperienceYears
        {
            get;
            set;
        }

        public string ExperienceDescription
        {
            get;
            set;
        }

        public string Skills
        {
            get;
            set;
        }

        public Dictionary<Guid, string> KeyWords
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
