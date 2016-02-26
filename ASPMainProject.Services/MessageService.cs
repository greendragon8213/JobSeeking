using ASPMainProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace ASPMainProject.Services
{
    public class MessageService
    {
        public List<ChatPreview> GetChatsPreviewByUserId(Guid userId)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                var chatPreviews = (from item in context.Chats
                        where item.UserId1 == userId || item.UserId2 == userId
                        select new ASPMainProject.Models.ChatPreview()
                        {
                            Id = item.Id,
                            InterlocutorName = (item.UserId1 == userId) ? item.Users1.UserName : item.Users.UserName                           
                        }).ToList();

                var userService = new UserService();
                foreach (var item in chatPreviews)
                {
                    var lastMessage = (from elem in context.Messages
                                        where elem.ChatId == item.Id
                                        select elem).ToList().OrderBy(m => m.Date).LastOrDefault();
                    if (lastMessage != null)
                    {
                        item.LastMessageDate = lastMessage.Date;
                        item.LastMessageContent = lastMessage.Text;
                    }

                    item.NewMessagesCount = GetNewMessageCountByChatId(item.Id, userId);
                        
                }

                return chatPreviews.OrderByDescending(m => m.LastMessageDate).ToList();
            }
        }

        //ToDo get last 20 messages!
        public Chat GetChatByChatId(Guid chatId, Guid currentUserId)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                var resChat = (from item in context.Chats
                        where item.Id == chatId
                        select new ASPMainProject.Models.Chat()
                        {                            
                            Id = item.Id,
                            InterlocutorId = (item.UserId1 == currentUserId) ? item.UserId2 : item.UserId1,
                            //ToDo change name to position or fullName
                            InterlocutorName = (item.UserId1 == currentUserId) ? item.Users1.UserName : item.Users.UserName,
                        }).FirstOrDefault();

                if (resChat == null)
                    return null;

                // Get messages by chat id
                var resMessages = (from elem in context.Messages
                                    where elem.ChatId == resChat.Id
                                   select elem).ToList();

                if (resMessages != null)
                    resMessages = resMessages.OrderBy(m => m.Date).ToList();

                resChat.Messages = new List<Message>();
                // Get messages
                foreach(var item in resMessages)
                {
                    resChat.Messages.Add(new Message() 
                    {
                        Id = item.Id,
                        AuthorId = item.AuthorId,
                        Date = item.Date,
                        Text = item.Text,
                        IsRead = item.IsRead
                    });
                }

                return resChat;
            }
        }

        //Returns chatId (because after sending we need to redirect to concrete conversation)
        public Guid SendMessageTo(Guid receiverId, Guid senderId, string messageText)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                //Get chatId by reseiverId and senderId
                var currentChatId = (from item in context.Chats
                               where item.UserId1 == receiverId && item.UserId2 == senderId ||
                                     item.UserId1 == senderId && item.UserId2 == receiverId
                               select item.Id).FirstOrDefault();

                if (currentChatId == Guid.Empty)
                {
                    var newChat = new SiteDataContext.Chats() {Id = Guid.NewGuid(), UserId1 = receiverId, UserId2 = senderId };
                    currentChatId = newChat.Id;
                    context.Chats.Add(newChat);
                    context.SaveChanges();
                }

                context.Messages.Add(new SiteDataContext.Messages() 
                { Id = Guid.NewGuid(), AuthorId = senderId, ChatId = currentChatId, Date = DateTime.Now, 
                    Text = messageText, IsRead = false });

                context.SaveChanges();
                return currentChatId;
            }
        }
        public int GetNewMessageCountByUserName(string userName)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
               var us = new UserService();
               Guid userId = us.GetUserIdByUserName(userName);
               List<Guid> usersChatIds = (from el in context.Chats
                        where el.UserId1 == userId || el.UserId2 == userId
                        select el).Select(x => x.Id).ToList();

               int resultCount = 0;
               foreach(var item in usersChatIds)
               {
                   int newMessagesInCurrentChatCount = 0;
                   newMessagesInCurrentChatCount = GetNewMessageCountByChatId(item, userId);
                   resultCount += newMessagesInCurrentChatCount;
               }
               return resultCount;
            }
        }

        public void MarkAllMessagesAsReadInChat(Guid chatId, Guid currentUserId)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                var elementsToChange = (from el in context.Messages
                                        where el.ChatId == chatId && !el.IsRead && el.AuthorId != currentUserId
                    select el.Id);


                foreach(var item in elementsToChange)
                {
                    context.Messages.Find(item).IsRead = true;
                }

                context.SaveChanges();
                //context.InsertOnSubmit(elementsToChange);
                //context.Messages.SubmitChanges();

            }
        }
        private int GetNewMessageCountByChatId(Guid chatId, Guid userId)
        {
            using (var context = new SiteDataContext.MyDataContext())
            {
                //ToDo
                return (from el in context.Messages
                        where el.ChatId == chatId && !el.IsRead && el.AuthorId != userId
                        select el).Count();
            }
        }

    }
}
