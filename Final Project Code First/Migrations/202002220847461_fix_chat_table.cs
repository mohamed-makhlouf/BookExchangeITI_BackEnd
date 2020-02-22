namespace Final_Project_Code_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_chat_table : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Chats", name: "ChatRecieverUser_UserId", newName: "Chat_Reciever_User_Id");
            RenameColumn(table: "dbo.Chats", name: "ChatSenderUser_UserId", newName: "Chat_Sender_User_Id");
            RenameIndex(table: "dbo.Chats", name: "IX_ChatSenderUser_UserId", newName: "IX_Chat_Sender_User_Id");
            RenameIndex(table: "dbo.Chats", name: "IX_ChatRecieverUser_UserId", newName: "IX_Chat_Reciever_User_Id");
            AddColumn("dbo.Chats", "Conversation_Id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chats", "Conversation_Id");
            RenameIndex(table: "dbo.Chats", name: "IX_Chat_Reciever_User_Id", newName: "IX_ChatRecieverUser_UserId");
            RenameIndex(table: "dbo.Chats", name: "IX_Chat_Sender_User_Id", newName: "IX_ChatSenderUser_UserId");
            RenameColumn(table: "dbo.Chats", name: "Chat_Sender_User_Id", newName: "ChatSenderUser_UserId");
            RenameColumn(table: "dbo.Chats", name: "Chat_Reciever_User_Id", newName: "ChatRecieverUser_UserId");
        }
    }
}
