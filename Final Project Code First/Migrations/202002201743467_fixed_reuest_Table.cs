namespace Final_Project_Code_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixed_reuest_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "Requested_Book_Id", c => c.Int());
            CreateIndex("dbo.Request", "Requested_Book_Id");
            AddForeignKey("dbo.Request", "Requested_Book_Id", "dbo.Book", "Book_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Request", "Requested_Book_Id", "dbo.Book");
            DropIndex("dbo.Request", new[] { "Requested_Book_Id" });
            DropColumn("dbo.Request", "Requested_Book_Id");
        }
    }
}
