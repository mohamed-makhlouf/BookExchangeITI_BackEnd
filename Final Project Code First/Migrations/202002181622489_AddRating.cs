namespace Final_Project_Code_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RateRatedUser_UserId = c.Int(),
                        RateSenderUser_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.RateRatedUser_UserId)
                .ForeignKey("dbo.User", t => t.RateSenderUser_UserId)
                .Index(t => t.RateRatedUser_UserId)
                .Index(t => t.RateSenderUser_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "RateSenderUser_UserId", "dbo.User");
            DropForeignKey("dbo.Ratings", "RateRatedUser_UserId", "dbo.User");
            DropIndex("dbo.Ratings", new[] { "RateSenderUser_UserId" });
            DropIndex("dbo.Ratings", new[] { "RateRatedUser_UserId" });
            DropTable("dbo.Ratings");
        }
    }
}
