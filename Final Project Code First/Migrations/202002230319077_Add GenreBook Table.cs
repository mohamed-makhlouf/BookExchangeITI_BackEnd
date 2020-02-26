namespace Final_Project_Code_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenreBookTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GenreBooks", newName: "GenreBook1");
            CreateTable(
                "dbo.Genre_Book",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Book_Id })
                .ForeignKey("dbo.Book", t => t.Book_Id, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.Genre_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Book_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Genre_Book", "Genre_Id", "dbo.Genre");
            DropForeignKey("dbo.Genre_Book", "Book_Id", "dbo.Book");
            DropIndex("dbo.Genre_Book", new[] { "Book_Id" });
            DropIndex("dbo.Genre_Book", new[] { "Genre_Id" });
            DropTable("dbo.Genre_Book");
            RenameTable(name: "dbo.GenreBook1", newName: "GenreBooks");
        }
    }
}
