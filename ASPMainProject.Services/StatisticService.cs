using ASPMainProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPMainProject.Services
{
    public class StatisticService
    {
        public ArrayList GetSomeStatisticData()
        {
            var result = new ArrayList()
            {   
                new ArrayList()
                {
                    "Keyword", "Candidates count", "Adverticements count"
                }
            };

            using (var context = new SiteDataContext.MyDataContext())
            {
                List<Guid> resKeyWordIds = (from item in context.KeyWords
                             select item.Id
                    ).Take(10).ToList();

                foreach(var element in resKeyWordIds)
                {
                    int candsCount = (from item in context.Candidates2KeyWords
                             where item.KeyWordsId == element 
                             select item.Id).ToList().Count;

                    int adsCount = (from item in context.Ads2KeyWords
                             where item.KeyWordId == element 
                             select item.Id).ToList().Count;

                    string keywordName = (from item in context.KeyWords
                                          where item.Id == element
                                          select item.Value).FirstOrDefault();

                    var singleStatisicRow = new ArrayList() { keywordName, candsCount, adsCount };
                    result.Add(singleStatisicRow);
                }
            }
            return result;
        }
    }
}
