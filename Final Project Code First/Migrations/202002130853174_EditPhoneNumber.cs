namespace Final_Project_Code_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditPhoneNumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Phone_Number", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Phone_Number", c => c.Int(nullable: false));
        }
    }
}
