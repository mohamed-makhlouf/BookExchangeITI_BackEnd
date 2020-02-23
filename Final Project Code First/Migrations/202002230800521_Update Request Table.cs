namespace Final_Project_Code_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRequestTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "Sender_Swap_Id", c => c.Int());
            CreateIndex("dbo.Request", "Sender_Swap_Id");
            AddForeignKey("dbo.Request", "Sender_Swap_Id", "dbo.User", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Request", "Sender_Swap_Id", "dbo.User");
            DropIndex("dbo.Request", new[] { "Sender_Swap_Id" });
            DropColumn("dbo.Request", "Sender_Swap_Id");
        }
    }
}
