using System;
using System.Linq;
using System.Web.Mvc;
using ASPMainProject.Models;
using ASPMainProject.Services;

namespace ASPMainProject.Controllers
{
    public class AdController : Controller
    {
        private readonly AdService _adService = new AdService();
        private readonly KeyWordsService _keyWordsService = new KeyWordsService();
        //
        // GET: /Ad/
        public ActionResult Index(Guid? employerId = null)
        {
            var model = new AdsWithKeyWords();

            //we dont use filter, we return all ads
            if (employerId == null)
                model.Ads = _adService.GetAllAds();
            else
                model.Ads = _adService.GetAdsByAuthorId(employerId ?? Guid.Empty);

            model.KeyWordPreviews = _keyWordsService.GetAllKeyWords();

            return View(model);
        }
        
        [HttpPost]
        public ActionResult Index(AdsWithKeyWords model)
        {
            if (model == null || model.KeyWordPreviews == null)
            {
                return RedirectToAction("Index");
            }

            var res = model.KeyWordPreviews.Where(f => f.Selected).ToList();
            if (res.Count() == 0)
                return RedirectToAction("Index");

            model.Ads = _adService.GetFilteredAds
                (res.Select(p => p.Id));
            return View(model);
        }
        public ActionResult Details(Guid adId)
        {
            //var model = new Ad() { Location = "Lviv" };
            var model = _adService.GetAdById(adId);
            return View(model);
        }

        public ActionResult DetailsForm(Guid adId)
        {
            //var model = new Ad() { Location = "Lviv" };
            var model = _adService.GetAdById(adId);

            var allKeyWords = _keyWordsService.GetAllKeyWords();
            foreach (var item in allKeyWords)
            {
                if (model.KeyWords.ContainsKey(item.Id))
                    item.Selected = true;
            }

            model.KeyWordPreviews = (allKeyWords);

            return View(model);
        }

        [HttpPost]
        public ActionResult DetailsForm(Ad ad)
        {
            ad.KeyWords = ad.KeyWordPreviews.Where(f => f.Selected).Select(x => x.Id).ToDictionary(item => item, item => "");
            var result = _adService.Update(ad);
            return RedirectToAction("Details", new { adId = ad.Id });
        }
	
        public ActionResult AddNewAd(/*Guid authorId*/)
        {
            var userService = new UserService();

            var es = new EmployerService();
            var currEmplId = es.GetEmployerIdByUserId
                (userService.GetUserIdByUserName(User.Identity.Name));

            if (currEmplId == Guid.Empty)
                return Content("You should log in or register at first");

            var res = _adService.AddNewAd(new Ad() { AuthorId = currEmplId });
            return RedirectToAction("DetailsForm", new { adId = res
                 });
        }
    }
}