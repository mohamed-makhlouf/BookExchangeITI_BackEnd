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
        
        public void sendMessage( string name,string message)
        {
            //dbcode
            Clients.All.newMessage(name , message);


        }
       
    }
}