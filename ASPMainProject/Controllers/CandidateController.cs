using System;
using System.Linq;
using System.Web.Mvc;
using ASPMainProject.Models;
using ASPMainProject.Services;

namespace ASPMainProject.Controllers
{
    public class CandidateController : Controller
    {
        private readonly CandidateService _candidateService = new CandidateService();
        private readonly KeyWordsService _keyWordsService = new KeyWordsService();
        //
        // GET: /Candidate/
        public ActionResult Index(/*List<CandidatePreview> candidatePreviews = null, List<KeyWordPreview> filterKeyWordPreviews = null*/)
        {       
            var model = new CandidateVM();

            //we dont use filter, we return all cands
            model.CandidatePreviews = (_candidateService.GetAllCandidates());
            model.KeyWordPreviews = (_keyWordsService.GetAllKeyWords());
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CandidateVM model)
        {
            if (model == null || model.KeyWordPreviews == null)
            {
                return RedirectToAction("Index");
            }

            var res = model.KeyWordPreviews.Where(f => f.Selected).ToList();
            if (res.Count() == 0)
                return RedirectToAction("Index");

            model.CandidatePreviews = _candidateService.GetFilteredCandidatePreviews
                (res.Select(p => p.Id));
            return View(model);
        }

        public ActionResult Details(Guid candidateId)
        {
            //cand details
            var model = _candidateService.GetCandidateById(candidateId);
            return View(model);
        }

        public ActionResult DetailsForm(Guid candidateId)
        {
            //edit cand details
            var model = _candidateService.GetCandidateById(candidateId);

            var allKeyWords = _keyWordsService.GetAllKeyWords();
            foreach(var item in allKeyWords)
            {
                if (model.KeyWords.ContainsKey(item.Id))
                    item.Selected = true;
            }

            model.KeyWordPreviews = (allKeyWords);
            return View(model);
        }

        [HttpPost]
        public ActionResult DetailsForm(Candidate candidate)
        {
            candidate.KeyWords = candidate.KeyWordPreviews.Where(f => f.Selected).Select(x => x.Id).ToDictionary(item => item, item => "");
            var result = _candidateService.Update(candidate);
            return RedirectToAction("Details", new { candidateId = candidate.Id });
        }
	}
}