using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Final_Project_Code_First
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        ChatEntities db = new ChatEntities();
        public void sendMessage( int senderId,string message)
        {

            //dbcode
            Clients.All.newMessage(senderId, message);
            Chat mess = new Chat() { ChatSenderUser_UserId = senderId, Message = message, Date_Of_Message = DateTime.Now };

            db.Chats.Add(mess);
            db.SaveChanges();

        }
       
    }
}