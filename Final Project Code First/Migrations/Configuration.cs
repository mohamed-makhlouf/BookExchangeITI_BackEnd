namespace Final_Project_Code_First.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Final_Project_Code_First.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Final_Project_Code_First.Models.BookExchangeModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Final_Project_Code_First.Models.BookExchangeModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.RequestStaus.AddOrUpdate(x => x.Id,
                Enum.GetValues(typeof(RequestStatusEnum))
                .OfType<RequestStatusEnum>()
                .Select(x => new RequestStaus { Id = x, Name = x.ToString() })
                .ToArray()
                );
            context.ChatStatuses.AddOrUpdate(x => x.Id,
                Enum.GetValues(typeof(ChatStatusEnum))
                    .OfType<ChatStatusEnum>()
                    .Select(x=> new ChatStatus { Id = x , Name = x.ToString()})
                    .ToArray()
                );
            context.BookConditions.AddOrUpdate(x => x.Id,
                Enum.GetValues(typeof(BookConditionEnum))
                    .OfType<BookConditionEnum>()
                    .Select(x=> new BookCondition { Id = x , Name = x.ToString() })
                    .ToArray()
            );
            context.userRoleTables.AddOrUpdate(x => x.Id,
                Enum.GetValues(typeof(UserRole))
                    .OfType<UserRole>()
                    .Select(x => new UserRoleTable { Id = x, Name = x.ToString() })
                    .ToArray()
            );
            

        }
    }
}
