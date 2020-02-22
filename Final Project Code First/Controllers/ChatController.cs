using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Final_Project_Code_First.Models;


namespace Final_Project_Code_First.Controllers
{
    public class ChatController : ApiController
    {
        private BookExchangeModel db = new BookExchangeModel();

        // GET: api/Chats
        //public IQueryable<Chat> GetChats()
        //{
        //    return db.Chats;
        //}
        [HttpGet]
        public IHttpActionResult GetChat(int sender, int receiver, int page)
        {
            var chats = db.Chats
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
    }
}
