using System;
using System.Collections.Generic;
using System.Linq;
using ASPMainProject.Models;

namespace ASPMainProject.Services
{
    public class EmployerService
    {
        public List<Employer> GetAllEmployers()
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                return (from item in context.Employers
                        select new Employer()
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            ApprovingState = (ApprovingState)item.Users.ApprovingStatus,
                            Company = item.Company,
                            FullName = item.FullName,
                            ContactInformation = item.ContactInformation
                        }).ToList();
            }
        }

        public Employer GetEmployerById(Guid id)
        {
            Employer employer;

            using (var context = new SiteDataContext.MyDataContext())
            {
                employer = (from item in context.Employers
                        where item.Id == id
                        select new Employer()
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            ApprovingState = (ApprovingState)item.Users.ApprovingStatus,
                            Company = item.Company,
                            FullName = item.FullName,
                            ContactInformation = item.ContactInformation,
                        }).FirstOrDefault();
            }

            var adService = new AdService();
            if (employer != null)
            {
                employer.Ads = adService.GetAdsByAuthorId(id);
                return employer;
            }

            return null;
        }

        public Guid GetEmployerIdByUserId(Guid userId)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                return (from item in context.Employers
                            where item.UserId == userId
                            select item.Id).FirstOrDefault();
            }

        }
        public bool Update(Employer employer)
        {
            var result = true;

            using (var context = new SiteDataContext.MyDataContext())
            {
                var dbEmployer = context.Employers.Find(employer.Id);

                if (dbEmployer == null) return false;

                dbEmployer.FullName = employer.FullName;
                dbEmployer.ContactInformation = employer.ContactInformation;
                dbEmployer.Company = employer.Company;

                var dbUser = context.Users.Find(employer.UserId);
                if (dbUser != null)
                    dbUser.ApprovingStatus = (int)employer.ApprovingState;
                else
                    return false;

                context.SaveChanges();
            }

            return result;
        }

        public Guid AddNewEmployer(Employer employer)
        {
            if (employer == null)
            {
                return Guid.Empty;
            }

            Guid result = Guid.Empty;

            using (var context = new SiteDataContext.MyDataContext())
            {
                var dbEmployer = context.Employers.Create();

                dbEmployer.Id = Guid.NewGuid();
                dbEmployer.UserId = employer.UserId;

                //we dont need to fill other fields, because user will be redirected to DetailsForm
                //dbEmployer.FullName = employer.FullName;
                //dbEmployer.ContactInformation = employer.ContactInformation;
                //dbEmployer.Company = employer.Company;

                context.Employers.Add(dbEmployer);

                context.SaveChanges();
                result = dbEmployer.Id;
            }

            return result;
        }
    }
}
