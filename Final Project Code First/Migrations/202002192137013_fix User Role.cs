namespace Final_Project_Code_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixUserRole : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.User", new[] { "UserRole_Id" });
            DropColumn("dbo.User", "User_Role_Id");
            RenameColumn(table: "dbo.User", name: "UserRole_Id", newName: "User_Role_Id");
            AlterColumn("dbo.User", "User_Role_Id", c => c.Int(defaultValue:0));
            CreateIndex("dbo.User", "User_Role_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "User_Role_Id" });
            AlterColumn("dbo.User", "User_Role_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.User", name: "User_Role_Id", newName: "UserRole_Id");
            AddColumn("dbo.User", "User_Role_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.User", "UserRole_Id");
        }
    }
}
