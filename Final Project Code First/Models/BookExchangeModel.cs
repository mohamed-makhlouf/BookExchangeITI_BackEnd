namespace Final_Project_Code_First.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BookExchangeModel : DbContext
    {
        // Your context has been configured to use a 'BookExchangeModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Final_Project_Code_First.Models.BookExchangeModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BookExchangeModel' 
        // connection string in the application configuration file.
        public BookExchangeModel()
            : base("name=BookExchangeModel")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().Property(book => book.Rate).HasPrecision(5, 3);
            modelBuilder.Entity<User>().Property(user => user.Rate).HasPrecision(5, 3);
            //modelBuilder.Entity<User>()
            //    .HasMany(ww => ww.UserWantBooks)
            //    .WithMany(ss => ss.UserWantBooks)
            //    .Map(ee => ee.ToTable("User_Want_Book"));

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
       
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoleTable> userRoleTables { get; set; }
        public DbSet<BookCondition> BookConditions { get; set; }
        public DbSet<UserHaveBook> UserHaveBooks { get; set; }
        public DbSet<UserWantBook> UserWantBooks { get; set; }
        public DbSet<RequestStaus> RequestStaus { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<ChatStatus> ChatStatuses { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<GenreBook> GenreBooks { get; set; }

        public DbSet<Rating> Ratings { get; set; }
        

    }

}