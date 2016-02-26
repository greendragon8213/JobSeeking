using System;
using System.Web.Mvc;
using ASPMainProject.Services;
using ASPMainProject.Models;

namespace ASPMainProject.Controllers
{
    public class EmployerController : Controller
    {
        private readonly EmployerService _employerService = new EmployerService();
        //
        // GET: /Employer/
        public ActionResult Index()
        {
            //var model = new List<Employer>() { new Employer() { Id = new Guid(), FullName = "Some Man", ApprovingState = ApprovingState.Approved }, new Employer() { Id = new Guid(), FullName = "Another Man", ApprovingState = ApprovingState.Approved } };
            var model = _employerService.GetAllEmployers();
            return View(model);
        }

        public ActionResult Details(Guid id)
        {
            //empl details
            //var model = new Employer() {Id = id, Company = "Box1" };
            var model = _employerService.GetEmployerById(id);
            return View(model);
        }

        public ActionResult DetailsForm(Guid id)
        {
            //edit empl details
            //var model = new Employer() {Id = id, Company = "COMPANY111111" };
            var model = _employerService.GetEmployerById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult DetailsForm(Employer employer)
        {
            //edit empl details
            //var model = new Employer() { Company = "COMPANY111111" };
            var result = _employerService.Update(employer);
            return RedirectToAction("Details", new { id = employer.Id });
        }
	}
}