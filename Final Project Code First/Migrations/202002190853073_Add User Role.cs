namespace Final_Project_Code_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User_Role",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "User_Role_Id", c => c.Int(nullable: false));
            AddColumn("dbo.User", "UserRole_Id", c => c.Int());
            CreateIndex("dbo.User", "UserRole_Id");
            AddForeignKey("dbo.User", "UserRole_Id", "dbo.User_Role", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "UserRole_Id", "dbo.User_Role");
            DropIndex("dbo.User", new[] { "UserRole_Id" });
            DropColumn("dbo.User", "UserRole_Id");
            DropColumn("dbo.User", "User_Role_Id");
            DropTable("dbo.User_Role");
        }
    }
}
