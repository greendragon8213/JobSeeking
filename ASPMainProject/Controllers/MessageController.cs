using System;
using System.Web.Mvc;
using ASPMainProject.Services;

namespace ASPMainProject.Controllers
{
    public class MessageController : Controller
    {
        readonly MessageService _messageService = new MessageService();
        readonly UserService _userservice = new UserService();
        //
        // GET: /Message/
        public ActionResult Index()
        {
            Guid currentUserId = _userservice.GetUserIdByUserName(User.Identity.Name); //new Guid("57013564-eb60-49b7-8155-345eac12ff93");
            var model = _messageService.GetChatsPreviewByUserId(currentUserId);
            return View(model);
        }

        //show one chat
        public ActionResult Chat(Guid chatId)
        {
            Guid currentUserId = _userservice.GetUserIdByUserName(User.Identity.Name);//new Guid("57013564-eb60-49b7-8155-345eac12ff93");
            var model = _messageService.GetChatByChatId(chatId, currentUserId);            
            model.CurrentUserId = currentUserId;

            //If we got so far, we have read messages in current chat
            _messageService.MarkAllMessagesAsReadInChat(chatId, currentUserId);

            return View(model);
        }

        [HttpPost]
        public ActionResult SendMessageTo(Guid receiverId, string messageText)
        {  
            Guid currentUserId = _userservice.GetUserIdByUserName(User.Identity.Name);//new Guid("57013564-eb60-49b7-8155-345eac12ff93");
            Guid chatId = _messageService.SendMessageTo(receiverId, currentUserId, messageText);
            return RedirectToAction("Chat", new { chatId = chatId });
        }
	}
}