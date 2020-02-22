using Final_Project_Code_First.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_Project_Code_First.Controllers
{
    public class ChatController : ApiController
    {
        private BookExchangeModel db = new BookExchangeModel();

        // GET: api/Chats
        public IQueryable<Chat> GetChats()
        {
            return db.Chats;
        }
        [HttpGet]
        public IHttpActionResult GetChat(int sender, int receiver, int page)
        {
            var chats = GetChats()
                        .Where(chat => chat.ChatSenderUser.UserId == sender && chat.ChatRecieverUser.UserId == receiver)
                        .OrderBy(chat => chat.DateOfMessage)
                        .Select(chat => new { SenderId=chat.ChatSenderUser.UserId, RecieverId=chat.ChatRecieverUser.UserId, chat.Message, chat.DateOfMessage, chat.ChatStatus.Name })
                        .Skip((page == 0 ? 1 : page) * 2)
                        .Take(2)
                        .ToList();
            if (chats.Count == 0)
            {
                return NotFound();
            }
            return Ok(chats);
        }
        [HttpPost]
        public IHttpActionResult PostChat(Chat chat)

        {

            if (chat == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            db.Chats.Add(chat);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [HttpDelete]
        public IHttpActionResult DeleteChat(int id, int sender)
        {
            var chat = db.Chats.Find(id);

            if (chat == null)
            {
                return NotFound();
            }
            else if (chat.ChatSenderUser.UserId != sender)
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }
            return Ok();
        }

        [HttpGet]
        [Route("api/chat/getcurrentuserchat")]
        public IHttpActionResult GetAllChatByLoggedInUser(int userId)
        {
            var chats = db.Chats
                .Where(chat => chat.ChatSenderUser.UserId == userId || chat.ChatRecieverUser.UserId == userId)
                .GroupBy(chat=> chat.ConversationId)
                .Select(group => new
                {
                    conversationId=group.Key,
                    chat_Id = group.Max(chat2=> chat2.Id),
                    chat =  group.Where(chat=> chat.Id == group.Max(chat2 => chat2.Id))
                                .Select(chat9 => new { 
                                                        chat9.Id,
                                                        SenderUser = new {
                                                                chat9.ChatSenderUser.FirstName,
                                                                chat9.ChatSenderUser.LastName,
                                                                chat9.ChatSenderUser.PhotoUrl,
                                                                chat9.ChatSenderUser.UserId
                                                        },
                                                        RecieverUser= new
                                                        {
                                                            chat9.ChatRecieverUser.FirstName,
                                                            chat9.ChatRecieverUser.LastName,
                                                            chat9.ChatRecieverUser.PhotoUrl,
                                                            chat9.ChatRecieverUser.UserId
                                                        },
                                                        chat9.DateOfMessage,
                                                        chat9.Message
                                                        }).FirstOrDefault()

                }).ToList();

           return Ok(chats);
        }
        [HttpGet]
        [Route("api/chat/getChatByConversationId")]
        public IHttpActionResult getChatByConversationId(string conversationId)
        {
            var chats = db.Chats
                .Where(chat => chat.ConversationId.Equals(conversationId))
                .OrderByDescending(chat=> chat.Id)
                .Select(chat => new
                {
                    chat.Id,
                    SenderUser = new
                    {
                        chat.ChatSenderUser.FirstName,
                        chat.ChatSenderUser.LastName,
                        chat.ChatSenderUser.PhotoUrl,
                        chat.ChatSenderUser.UserId
                    },
                    RecieverUser = new
                    {
                        chat.ChatRecieverUser.FirstName,
                        chat.ChatRecieverUser.LastName,
                        chat.ChatRecieverUser.PhotoUrl,
                        chat.ChatRecieverUser.UserId
                    },
                    chat.DateOfMessage,
                    chat.Message
                })
                .ToList();
            return Ok(chats);
        }

    }
}
