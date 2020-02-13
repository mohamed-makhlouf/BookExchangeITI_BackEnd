namespace Final_Project_Code_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Book_Conditions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        Book_Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 5, scale: 3),
                        Photo_Url = c.String(nullable: false),
                        Author_Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Book_Id);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false, identity: true),
                        Genre_Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Genre_Id);
            
            CreateTable(
                "dbo.User_Have_Book",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                        Book_Condtion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Book_Id })
                .ForeignKey("dbo.Book", t => t.Book_Id, cascadeDelete: true)
                .ForeignKey("dbo.Book_Conditions", t => t.Book_Condtion, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Book_Id)
                .Index(t => t.Book_Condtion);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        First_Name = c.String(nullable: false),
                        Last_Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Blocked = c.Boolean(nullable: false),
                        Rate = c.Decimal(precision: 5, scale: 3),
                        City = c.String(),
                        Address = c.String(),
                        Phone_Number = c.Int(nullable: false),
                        Photo_Url = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Chat_Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        Date_Of_Message = c.DateTime(nullable: false),
                        Chat_Status_Id = c.Int(nullable: false),
                        ChatRecieverUser_UserId = c.Int(),
                        ChatSenderUser_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Chat_Id)
                .ForeignKey("dbo.Chat_Status", t => t.Chat_Status_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.ChatRecieverUser_UserId)
                .ForeignKey("dbo.User", t => t.ChatSenderUser_UserId)
                .Index(t => t.Chat_Status_Id)
                .Index(t => t.ChatRecieverUser_UserId)
                .Index(t => t.ChatSenderUser_UserId);
            
            CreateTable(
                "dbo.Chat_Status",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Report",
                c => new
                    {
                        Report_Id = c.Int(nullable: false, identity: true),
                        Complain = c.String(nullable: false),
                        Report_Sender_User_Id = c.Int(nullable: false),
                        Report_Reported_User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Report_Id)
                .ForeignKey("dbo.User", t => t.Report_Reported_User_Id)
                .ForeignKey("dbo.User", t => t.Report_Sender_User_Id, cascadeDelete: true)
                .Index(t => t.Report_Sender_User_Id)
                .Index(t => t.Report_Reported_User_Id);
            
            CreateTable(
                "dbo.Request",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Request_Status_Id = c.Int(nullable: false),
                        DateOfMessage = c.DateTime(nullable: false),
                        Swap = c.Boolean(),
                        BookId = c.Int(nullable: false),
                        Request_Sender_Id = c.Int(),
                        Request_Reciever_Id = c.Int(),
                        RequestStaus_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.RequestStaus", t => t.RequestStaus_Id)
                .ForeignKey("dbo.User", t => t.Request_Reciever_Id)
                .ForeignKey("dbo.User", t => t.Request_Sender_Id)
                .Index(t => t.BookId)
                .Index(t => t.Request_Sender_Id)
                .Index(t => t.Request_Reciever_Id)
                .Index(t => t.RequestStaus_Id);
            
            CreateTable(
                "dbo.RequestStaus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreBooks",
                c => new
                    {
                        Genre_Genre_Id = c.Int(nullable: false),
                        Book_Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Genre_Id, t.Book_Book_Id })
                .ForeignKey("dbo.Genre", t => t.Genre_Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Book", t => t.Book_Book_Id, cascadeDelete: true)
                .Index(t => t.Genre_Genre_Id)
                .Index(t => t.Book_Book_Id);
            
            CreateTable(
                "dbo.BookUsers",
                c => new
                    {
                        Book_Book_Id = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_Book_Id, t.User_UserId })
                .ForeignKey("dbo.Book", t => t.Book_Book_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Book_Book_Id)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookUsers", "User_UserId", "dbo.User");
            DropForeignKey("dbo.BookUsers", "Book_Book_Id", "dbo.Book");
            DropForeignKey("dbo.User_Have_Book", "User_Id", "dbo.User");
            DropForeignKey("dbo.Request", "Request_Sender_Id", "dbo.User");
            DropForeignKey("dbo.Report", "Report_Sender_User_Id", "dbo.User");
            DropForeignKey("dbo.Chats", "ChatSenderUser_UserId", "dbo.User");
            DropForeignKey("dbo.Request", "Request_Reciever_Id", "dbo.User");
            DropForeignKey("dbo.Request", "RequestStaus_Id", "dbo.RequestStaus");
            DropForeignKey("dbo.Request", "BookId", "dbo.Book");
            DropForeignKey("dbo.Report", "Report_Reported_User_Id", "dbo.User");
            DropForeignKey("dbo.Chats", "ChatRecieverUser_UserId", "dbo.User");
            DropForeignKey("dbo.Chats", "Chat_Status_Id", "dbo.Chat_Status");
            DropForeignKey("dbo.User_Have_Book", "Book_Condtion", "dbo.Book_Conditions");
            DropForeignKey("dbo.User_Have_Book", "Book_Id", "dbo.Book");
            DropForeignKey("dbo.GenreBooks", "Book_Book_Id", "dbo.Book");
            DropForeignKey("dbo.GenreBooks", "Genre_Genre_Id", "dbo.Genre");
            DropIndex("dbo.BookUsers", new[] { "User_UserId" });
            DropIndex("dbo.BookUsers", new[] { "Book_Book_Id" });
            DropIndex("dbo.GenreBooks", new[] { "Book_Book_Id" });
            DropIndex("dbo.GenreBooks", new[] { "Genre_Genre_Id" });
            DropIndex("dbo.Request", new[] { "RequestStaus_Id" });
            DropIndex("dbo.Request", new[] { "Request_Reciever_Id" });
            DropIndex("dbo.Request", new[] { "Request_Sender_Id" });
            DropIndex("dbo.Request", new[] { "BookId" });
            DropIndex("dbo.Report", new[] { "Report_Reported_User_Id" });
            DropIndex("dbo.Report", new[] { "Report_Sender_User_Id" });
            DropIndex("dbo.Chats", new[] { "ChatSenderUser_UserId" });
            DropIndex("dbo.Chats", new[] { "ChatRecieverUser_UserId" });
            DropIndex("dbo.Chats", new[] { "Chat_Status_Id" });
            DropIndex("dbo.User_Have_Book", new[] { "Book_Condtion" });
            DropIndex("dbo.User_Have_Book", new[] { "Book_Id" });
            DropIndex("dbo.User_Have_Book", new[] { "User_Id" });
            DropTable("dbo.BookUsers");
            DropTable("dbo.GenreBooks");
            DropTable("dbo.RequestStaus");
            DropTable("dbo.Request");
            DropTable("dbo.Report");
            DropTable("dbo.Chat_Status");
            DropTable("dbo.Chats");
            DropTable("dbo.User");
            DropTable("dbo.User_Have_Book");
            DropTable("dbo.Genre");
            DropTable("dbo.Book");
            DropTable("dbo.Book_Conditions");
        }
    }
}
