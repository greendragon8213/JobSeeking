﻿using System;
using System.Collections.Generic;

namespace ASPMainProject.Models
{
    public class CandidatePreview
    {
        public Guid Id
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

        public int NewMessagesCount { get; set; }

    }
}
