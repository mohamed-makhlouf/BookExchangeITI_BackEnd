namespace Final_Project_Code_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixratingtable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Ratings", name: "RateRatedUser_UserId", newName: "Rated_User_Id");
            RenameColumn(table: "dbo.Ratings", name: "RateSenderUser_UserId", newName: "Sender_User_Id");
            RenameIndex(table: "dbo.Ratings", name: "IX_RateSenderUser_UserId", newName: "IX_Sender_User_Id");
            RenameIndex(table: "dbo.Ratings", name: "IX_RateRatedUser_UserId", newName: "IX_Rated_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Ratings", name: "IX_Rated_User_Id", newName: "IX_RateRatedUser_UserId");
            RenameIndex(table: "dbo.Ratings", name: "IX_Sender_User_Id", newName: "IX_RateSenderUser_UserId");
            RenameColumn(table: "dbo.Ratings", name: "Sender_User_Id", newName: "RateSenderUser_UserId");
            RenameColumn(table: "dbo.Ratings", name: "Rated_User_Id", newName: "RateRatedUser_UserId");
        }
    }
}
