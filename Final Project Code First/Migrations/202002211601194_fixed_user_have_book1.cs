namespace Final_Project_Code_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixed_user_have_book1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User_Have_Book", "DateOfAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User_Have_Book", "DateOfAdded");
        }
    }
}
