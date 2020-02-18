namespace Final_Project_Code_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BookUsers", newName: "User_Want_Book");
            AddColumn("dbo.Book", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "Description");
            RenameTable(name: "dbo.User_Want_Book", newName: "BookUsers");
        }
    }
}
