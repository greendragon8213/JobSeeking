using System;

namespace ASPMainProject.Models
{
    public enum Role { User, Admin };
    public enum Position { Candidate, Employer };
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Position Position { get; set; }
        public ApprovingState ApprovingState { get; set; }

    }
}
