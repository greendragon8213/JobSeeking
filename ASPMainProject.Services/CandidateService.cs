using System;
using System.Collections.Generic;
using System.Linq;
using ASPMainProject.Models;

namespace ASPMainProject.Services
{
    /// <summary>
    /// This class extracts some data from DB and give back to CandidateController as  Models
    /// </summary>
    public class CandidateService
    {
        /// <summary>
        /// Returns Candidates as CandidatePreview from DB
        /// </summary>
        /// <param name="args">Аргумент метода Main()</param>
        public List<ASPMainProject.Models.CandidatePreview> GetAllCandidates()
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                var resCand = (from item in context.Candidates
                    select new ASPMainProject.Models.CandidatePreview()
                    {
                        Id = item.Id,
                        ApprovingState = (ApprovingState) item.Users.ApprovingStatus,
                        ExpectedPosition = item.ExpectedPosition,
                        ExperienceYears = (item.ExperienceYears) ?? default(int),                     
                        Location = item.Location,
                        SalaryPerMonth = item.SalaryPerMonth ?? default(int),
                        Skills = item.Skills
                    }).ToList();

                var keyWordsService = new KeyWordsService();
                foreach(var item in resCand)
                {
                    item.KeyWords = keyWordsService.GetCandidateKeyWords(item.Id);
                }

                return resCand;
            }

        }

        public ASPMainProject.Models.Candidate GetCandidateById(Guid id)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                var resCandidate = (from item in context.Candidates
                        where item.Id == id
                        select new ASPMainProject.Models.Candidate()
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            ApprovingState = (ApprovingState)item.Users.ApprovingStatus,
                            ExpectedPosition = item.ExpectedPosition,
                            ExperienceYears = (item.ExperienceYears) ?? default(int),
                            ExperienceDescription = item.ExperienceDescription,
                            Location = item.Location,
                            SalaryPerMonth = item.SalaryPerMonth ?? default(int),
                            Skills = item.Skills
                        }).FirstOrDefault();

                var keyWordsService = new KeyWordsService();
                resCandidate.KeyWords = keyWordsService.GetCandidateKeyWords(id);
                return resCandidate;
            }
        }
        public Guid GetCandidateIdByUserId(Guid userId)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                return (from item in context.Candidates 
                        where item.UserId == userId
                        select item.Id).FirstOrDefault();
            }

        }
        /// <summary>
        /// Returns Candidate as CandidatePreview from DB by KeywordsId.
        /// </summary>
        /// <param name="filterKeyWordsId"> Candidate must belong to all this keywords</param>
        public List<CandidatePreview> GetFilteredCandidatePreviews(IEnumerable<Guid> filterKeyWordsId)
        {
            if (filterKeyWordsId == null || filterKeyWordsId.Count() == 0)
                return null;

            HashSet<Guid> resultCandidateIds = null;
            KeyWordsService keyWordsService = new KeyWordsService();

            foreach(var item in filterKeyWordsId)
            {
                HashSet<Guid> currentResult = keyWordsService.GetCandidatesIdsByKeyWordId(item);

                //if it is the first iteration
                if (resultCandidateIds == null)
                {
                    resultCandidateIds = new HashSet<Guid>();
                    resultCandidateIds.UnionWith(currentResult);
                }
                else
                {
                    resultCandidateIds.IntersectWith(currentResult);
                }
            }

            //Get candidatesPreviews by candidate Ids
            var resultCandidatePreview = new List<CandidatePreview>();
            foreach(var item in resultCandidateIds)
            {
                resultCandidatePreview.Add(GetCandidatePreviewsById(item));
            }

            return resultCandidatePreview;
            ////____________
            ////var categoryCandidates = new Dictionary<Guid, string>();
            ////var filteredCandidates = new List<CandidatePreview>();

            ////if (filterCategoriesId == null || filterCategoriesId.Count() <= 0) return null;

            ////foreach (var item in filterCategoriesId)
            ////{
            ////    var keyWords2Candidates = new 
            ////    var _category2CandidateService = new Category2CandidateService();
            ////    var _candidateService = new CandidateService();
            ////    categoryCandidates = _category2CandidateService.GetByCategoryId(item).ToList();
            ////    foreach (var category2Candidate in categoryCandidates)
            ////    {
            ////        var foundCandidate = _candidateService.Find(category2Candidate.CandidateId);
            ////        var exist = filteredCandidates.Exists(element => element.Id == foundCandidate.Id);
            ////        if (!exist) filteredCandidates.Add(foundCandidate);
            ////    }
            ////}

            ////// ToDo Remove duplicate candidates from List, Distinct not working, 
            ////return filteredCandidates;
        }

        private CandidatePreview GetCandidatePreviewsById(Guid candidateId)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                var resCand = (from item in context.Candidates
                               where item.Id == candidateId
                               select new ASPMainProject.Models.CandidatePreview()
                               {
                                   Id = item.Id,
                                   ApprovingState = (ApprovingState)item.Users.ApprovingStatus,
                                   ExpectedPosition = item.ExpectedPosition,
                                   ExperienceYears = (item.ExperienceYears) ?? default(int),
                                   Location = item.Location,
                                   SalaryPerMonth = item.SalaryPerMonth ?? default(int),
                                   Skills = item.Skills
                               }).FirstOrDefault();

                var keyWordsService = new KeyWordsService();
                resCand.KeyWords = keyWordsService.GetCandidateKeyWords(resCand.Id);

                return resCand;
            }
        }
        public bool Update(Candidate candidate)
        {
            var result = true;

            using (var context = new SiteDataContext.MyDataContext())
            {
                var dbCandidate = context.Candidates.Find(candidate.Id);

                if (dbCandidate == null) 
                    return false;

                dbCandidate.ExpectedPosition = candidate.ExpectedPosition;
                dbCandidate.Location = candidate.Location;
                dbCandidate.SalaryPerMonth = candidate.SalaryPerMonth;
                dbCandidate.ExperienceYears = candidate.ExperienceYears;
                dbCandidate.ExperienceDescription = candidate.ExperienceDescription;
                dbCandidate.Skills = candidate.Skills;

                //We should delete all keywords before adding new!!!!!
                // (to avoid dublicate and old values)
                #region delete all cand keywords before adding new
                var toDelete = (from element in context.Candidates2KeyWords
                                where element.CandidateId == candidate.Id //&& element.KeyWordsId == item.Key
                                select element).ToList();

                foreach (var el in toDelete)
                    context.Candidates2KeyWords.Remove(el);

                context.SaveChanges();
                #endregion

                if (candidate.KeyWords != null)
                    foreach(var item in candidate.KeyWords)
                    {
                        if (context.KeyWords.Find(item.Key) == null)
                            continue;

                        context.Candidates2KeyWords.Add(
                                new SiteDataContext.Candidates2KeyWords()
                                {
                                    Id = Guid.NewGuid(),
                                    KeyWordsId = item.Key,
                                    CandidateId = candidate.Id
                                });
                    }

                var dbUser = context.Users.Find(candidate.UserId);
                if (dbUser != null)
                    dbUser.ApprovingStatus = (int)candidate.ApprovingState;
                else
                    return false;

                context.SaveChanges();
            }

            return result;
        }

        public Guid AddNewCandidate(Candidate candidate)
        {
            if (candidate.UserId == null)
                return Guid.Empty;

            Guid result = Guid.Empty;

            using (var context = new SiteDataContext.MyDataContext())
            {
                var dbCandidate = context.Candidates.Create();

                dbCandidate.Id = Guid.NewGuid();
                candidate.Id = dbCandidate.Id;
                dbCandidate.UserId = candidate.UserId;

                //I dont need this stuff because user will be redirected to DetailsForm
                //dbCandidate.Location = candidate.Location;
                //dbCandidate.SalaryPerMonth = candidate.SalaryPerMonth;
                //dbCandidate.Skills = candidate.Skills;
                //dbCandidate.ExperienceYears = candidate.ExperienceYears;
                //dbCandidate.ExperienceDescription = candidate.ExperienceDescription;
                //dbCandidate.ExpectedPosition = candidate.ExpectedPosition;
                //////dbCandidate.Candidates2KeyWords = new SiteDataContext.Candidates2KeyWords(){}
                
                context.Candidates.Add(dbCandidate);

                context.SaveChanges();

                //if (Update(candidate))
                result = dbCandidate.Id;
            }

            return result;
        }
    }
}
