using ASPMainProject.Models;
using System;
using System.Linq;

namespace ASPMainProject.Services
{
    public class UserService
    {
        public bool Authorize(string userName, string password)
        {
            var result = true;

            using (var context = new SiteDataContext.MyDataContext())
            {
                var resUser = (from item in context.Users
                                 where item.UserName == userName && item.Password == password
                                 select item).FirstOrDefault();

                if (resUser == null) 
                    return false;
            }

            return result;
        }
        public bool Update(User user)
        {
            var result = true;

            using (var context = new SiteDataContext.MyDataContext())
            {
                var dbUser = context.Users.Find(user.Id);

                if (dbUser == null) return false;

                dbUser.UserName = user.UserName;
                dbUser.Password = user.Password;
                dbUser.Role = (int) user.Role;
                dbUser.Position = (int)user.Position;

                context.SaveChanges();

                //result.Data.Add("id", dbUser.id);
            }

            return result;
        }
        public Guid Add(User user)
        {
            // ToDo depending on userRole create Candidate/Employer
            using (var context = new SiteDataContext.MyDataContext())
            {
                //if user with this name exist than break
                if (GetUserByUserName(user.UserName)!=null)
                    return Guid.Empty;

                var dbUser = context.Users.Create();

                dbUser.Id = Guid.NewGuid();
                dbUser.UserName = user.UserName;
                dbUser.Password = user.Password;
                dbUser.Role = (int)user.Role;
                dbUser.Position = (int)user.Position;
                dbUser.ApprovingStatus = 0;

                context.Users.Add(dbUser);

                context.SaveChanges();

                //result.Data.Add("id", dbUser.id);
                return dbUser.Id;
            }
        }
        public User GetUserByUserId(Guid userId)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                return (from item in context.Users
                        where item.Id == userId
                        select new User()
                        {
                            Id = item.Id,
                            UserName = item.UserName,
                            Role = (Role) item.Role,
                            Position = (Position)item.Position,
                            Password = item.Password,
                            ApprovingState = (ApprovingState) item.ApprovingStatus
                        }).FirstOrDefault();
            }
        }
        public User GetUserByUserName(string userName)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                return (from item in context.Users
                        where item.UserName == userName
                        select new User()
                        {
                            Id = item.Id,
                            UserName = item.UserName,
                            Role = (Role)item.Role,
                            Position = (Position)item.Position,
                            Password = item.Password,
                            ApprovingState = (ApprovingState)item.ApprovingStatus
                        }).FirstOrDefault();
            }
        }
        public Guid GetUserIdByUserName(string userName)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                return (from item in context.Users
                        where item.UserName == userName
                        select item.Id).FirstOrDefault();
            }
        }
    }
}
