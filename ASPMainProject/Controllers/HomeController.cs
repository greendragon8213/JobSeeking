using System.Web.Mvc;
using ASPMainProject.Services;

namespace ASPMainProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ToDo send it to other 
            if (User.Identity.IsAuthenticated)
            {
                var messageservice = new MessageService();

                ViewBag.NewMessagesInAllChatsCount = 
                        messageservice.GetNewMessageCountByUserName(User.Identity.Name);
            }

            // ToDo send data to display statistics
            var ss = new StatisticService();
            var model = ss.GetSomeStatisticData();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}