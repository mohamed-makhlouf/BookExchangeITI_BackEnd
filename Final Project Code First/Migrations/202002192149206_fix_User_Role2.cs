namespace Final_Project_Code_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_User_Role2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "User_Role_Id", "dbo.User_Role");
            DropIndex("dbo.User", new[] { "User_Role_Id" });
            AlterColumn("dbo.User", "User_Role_Id", c => c.Int(nullable: true));
            CreateIndex("dbo.User", "User_Role_Id");
            AddForeignKey("dbo.User", "User_Role_Id", "dbo.User_Role", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "User_Role_Id", "dbo.User_Role");
            DropIndex("dbo.User", new[] { "User_Role_Id" });
            AlterColumn("dbo.User", "User_Role_Id", c => c.Int());
            CreateIndex("dbo.User", "User_Role_Id");
            AddForeignKey("dbo.User", "User_Role_Id", "dbo.User_Role", "Id");
        }
    }
}
