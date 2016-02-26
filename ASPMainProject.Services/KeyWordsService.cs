using ASPMainProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPMainProject.Services
{
    public class KeyWordsService
    {
        public List<KeyWordPreview> GetAllKeyWords()
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                var res = (from item in context.KeyWords
                           select item).ToList();

                var result = new List<KeyWordPreview>();
                foreach(var item in res)
                {
                    result.Add(new KeyWordPreview() {Id = item.Id, Value = item.Value, Selected = false });
                }

                return result;
            }
        }
        public Dictionary<Guid, string> GetCandidateKeyWords(Guid candidateId)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                var keyWordsIds = (from item in context.Candidates2KeyWords
                                   where item.CandidateId == candidateId
                                   select item.KeyWordsId
                        ).ToList().Distinct();

                Dictionary<Guid, string> resultKeyWords = new Dictionary<Guid, string>();
                foreach (var item in keyWordsIds)
                {
                    resultKeyWords.Add(item, (from elem in context.KeyWords
                                              where elem.Id == item
                                              select elem.Value
                       ).FirstOrDefault());
                }

                return resultKeyWords;//new HashSet<string>(resultKeyWords.Values);                                   
            }
        }

        public Dictionary<Guid, string> GetAdKeyWords(Guid adId)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                var keyWordsIds = (from item in context.Ads2KeyWords
                                   where item.AdId == adId
                                   select item.KeyWordId
                        ).ToList().Distinct();

                Dictionary<Guid, string> resultKeyWords = new Dictionary<Guid, string>();
                foreach (var item in keyWordsIds)
                {
                    resultKeyWords.Add(item, (from elem in context.KeyWords
                                              where elem.Id == item
                                              select elem.Value
                       ).FirstOrDefault());
                }

                return resultKeyWords;//new HashSet<string>(resultKeyWords.Values);                                   
            }
        }

        public HashSet<Guid> GetCandidatesIdsByKeyWordId(Guid keyWordId)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                return new HashSet<Guid>((from item in context.Candidates2KeyWords
                                   where item.KeyWordsId == keyWordId
                                   select item.CandidateId
                        ).ToList().Distinct());                                  
            }
        }

        internal HashSet<Guid> GetAdsIdsByKeyWordId(Guid keyWordId)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                return new HashSet<Guid>((from item in context.Ads2KeyWords
                                          where item.KeyWordId == keyWordId
                                          select item.AdId
                        ).ToList().Distinct());
            }
        }
    }
}
