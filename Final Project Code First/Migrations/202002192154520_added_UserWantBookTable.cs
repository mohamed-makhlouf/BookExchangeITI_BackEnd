namespace Final_Project_Code_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_UserWantBookTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User_Want_Book", "Book_Book_Id", "dbo.Book");
            DropForeignKey("dbo.User_Want_Book", "User_UserId", "dbo.User");
            DropIndex("dbo.User_Want_Book", new[] { "Book_Book_Id" });
            DropIndex("dbo.User_Want_Book", new[] { "User_UserId" });
            DropTable("dbo.User_Want_Book");

            CreateTable(
                "dbo.User_Want_Book",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                        Date_Added = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Book_Id })
                .ForeignKey("dbo.Book", t => t.Book_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Book_Id);
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.User_Want_Book",
                c => new
                    {
                        Book_Book_Id = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_Book_Id, t.User_UserId });
            
            DropForeignKey("dbo.User_Want_Book", "User_Id", "dbo.User");
            DropForeignKey("dbo.User_Want_Book", "Book_Id", "dbo.Book");
            DropIndex("dbo.User_Want_Book", new[] { "Book_Id" });
            DropIndex("dbo.User_Want_Book", new[] { "User_Id" });
            DropTable("dbo.User_Want_Book");
            CreateIndex("dbo.User_Want_Book", "User_UserId");
            CreateIndex("dbo.User_Want_Book", "Book_Book_Id");
            AddForeignKey("dbo.User_Want_Book", "User_UserId", "dbo.User", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.User_Want_Book", "Book_Book_Id", "dbo.Book", "Book_Id", cascadeDelete: true);
        }
    }
}
