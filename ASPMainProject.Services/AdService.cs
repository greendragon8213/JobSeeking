using System;
using System.Collections.Generic;
using System.Linq;
using ASPMainProject.Models;

namespace ASPMainProject.Services
{
    public class AdService
    {
        public List<Ad> GetAllAds()
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                var res = (from item in context.Ads
                           //where item.record_state != 1
                           select new Ad()
                           {
                               Id = item.Id,
                               AuthorId = item.AuthorId,
                               Content = item.Content,
                               CreationDate = item.CreationDate,
                               Company = item.Company,
                               ActualState = (ActualState)item.ActualState,
                               Position = item.Position,
                               AuthorFullName = item.Employers.FullName,
                               Location = item.Location,
                               SalaryPerMonth = item.SalaryPerMonth ?? default(int)
                           }).ToList();
                var keyWordsService = new KeyWordsService();
                foreach (var item in res)
                {
                    item.KeyWords = keyWordsService.GetAdKeyWords(item.Id);
                }

                return res;
            }
        }

        public Ad GetAdById(Guid id)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                var resultAd = (from item in context.Ads
                                where item.Id == id
                                select new Ad()
                                {
                                    Id = item.Id,
                                    AuthorId = item.AuthorId,
                                    Content = item.Content,
                                    CreationDate = item.CreationDate,
                                    Company = item.Company,
                                    ActualState = (ActualState)item.ActualState,
                                    Position = item.Position,
                                    AuthorFullName = item.Employers.FullName,
                                    Location = item.Location,
                                    SalaryPerMonth = item.SalaryPerMonth ?? default(int)
                                }).FirstOrDefault();

                resultAd.AuthorUserId = (from item in context.Employers
                                         where item.Id == resultAd.AuthorId
                                         select item.UserId
                                         ).FirstOrDefault();
                var keyWordsService = new KeyWordsService();
                resultAd.KeyWords = keyWordsService.GetAdKeyWords(id);
                return resultAd;
            }
        }

        public List<Ad> GetAdsByAuthorId(Guid id)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                var adIds = (from item in context.Ads
                             where item.AuthorId == id
                             select item.Id).ToList();

                var result = new List<Ad>();
                foreach (var item in adIds)
                {
                    result.Add(GetAdById(item));
                }
                return result;
            }
        }

        ////public HashSet<string> GetAdKeyWords(Guid adId)
        ////{
        ////    using (var context = new SiteDataContext.MyDataContext())
        ////    {
        ////        var keyWordsIds = (from item in context.Ads2KeyWords
        ////                           where item.AdId == adId
        ////                           select item.KeyWordId
        ////                ).ToList();

        ////        Dictionary<Guid, string> resultKeyWords = new Dictionary<Guid, string>();
        ////        foreach (var item in keyWordsIds)
        ////        {
        ////            resultKeyWords.Add(item, (from elem in context.KeyWords
        ////                                      where elem.Id == item
        ////                                      select elem.Value
        ////               ).FirstOrDefault());
        ////        }

        ////        return new HashSet<string>(resultKeyWords.Values);
        ////    }
        ////}

        public bool Update(Ad ad)
        {
            var result = true;

            using (var context = new SiteDataContext.MyDataContext())
            {
                var dbAd = context.Ads.Find(ad.Id);

                if (dbAd == null) return false;

                dbAd.Content = ad.Content;
                dbAd.Company = ad.Company;
                dbAd.ActualState = (int)ad.ActualState;
                dbAd.Location = ad.Location;
                dbAd.Position = ad.Position;
                dbAd.SalaryPerMonth = ad.SalaryPerMonth;

                #region delete all ad keywords before adding new
                var toDelete = (from element in context.Ads2KeyWords
                                where element.AdId == ad.Id //&& element.KeyWordsId == item.Key
                                select element).ToList();

                foreach (var el in toDelete)
                    context.Ads2KeyWords.Remove(el);

                context.SaveChanges();
                #endregion

                if (ad.KeyWords != null)
                    foreach (var item in ad.KeyWords)
                    {
                        if (context.KeyWords.Find(item.Key) == null)
                            continue;

                        context.Ads2KeyWords.Add(
                                new SiteDataContext.Ads2KeyWords()
                                {
                                    Id = Guid.NewGuid(),
                                    KeyWordId = item.Key,
                                    AdId = ad.Id
                                });
                    }

                context.SaveChanges();
            }

            return true;
        }

        public Guid AddNewAd(Ad ad)
        {
            if (ad.AuthorId == Guid.Empty)
                return Guid.Empty;

            var result = Guid.Empty;

            using (var context = new SiteDataContext.MyDataContext())
            {
                var dbAd = context.Ads.Create();

                dbAd.Id = Guid.NewGuid();
                result = dbAd.Id;
                dbAd.AuthorId = ad.AuthorId;
                dbAd.CreationDate = DateTime.Now;
                dbAd.ActualState = (int)ActualState.Actual;
                dbAd.Company = context.Employers.Find(ad.AuthorId).Company;

                //I dont need to fill other fields because empl will be redirected to AdDetailsForm:

                context.Ads.Add(dbAd);

                context.SaveChanges();
            }

            return result;
        }

        public List<Ad> GetFilteredAds(IEnumerable<Guid> filterKeyWordsId)
        {
            if (filterKeyWordsId == null || !filterKeyWordsId.Any())
                return null;

            HashSet<Guid> resultAdsIds = null;
            var keyWordsService = new KeyWordsService();

            foreach (var item in filterKeyWordsId)
            {
                HashSet<Guid> currentResult = keyWordsService.GetAdsIdsByKeyWordId(item);

                //if it is the first iteration
                if (resultAdsIds == null)
                {
                    resultAdsIds = new HashSet<Guid>();
                    resultAdsIds.UnionWith(currentResult);
                }
                else
                {
                    resultAdsIds.IntersectWith(currentResult);
                }
            }

            //Get candidatesPreviews by candidate Ids
            var resultAds = new List<Ad>();
            if (resultAdsIds != null)
                resultAds.AddRange(resultAdsIds.Select(GetAdById));

            return resultAds;
        }
    }
}
