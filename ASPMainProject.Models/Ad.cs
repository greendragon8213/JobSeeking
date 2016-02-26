using System;
using System.Collections.Generic;
using ASPMainProject.Models;

namespace ASPMainProject.Models
{
    public enum ActualState { Actual, UnActual}
    public class Ad
    {
        public Guid Id
        {
            get;
            set;
        }

        public Guid AuthorId
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public DateTime CreationDate
        {
            get;
            set;
        }

        public string Company
        {
            get;
            set;
        }

        public ActualState ActualState
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }

        public string Position
        {
            get;
            set;
        }
        public int SalaryPerMonth
        {
            get;
            set;
        }

        public string AuthorFullName { get; set; }

        public Guid AuthorUserId { get; set; }

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
