using System.Data.Entity;
using System.Data.Entity.Migrations;
using Common.DataModels;

namespace Data.Context
{
    public class DataInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var room = new Room {Name = "DotNetters"};

            context.Rooms.AddOrUpdate(room);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}