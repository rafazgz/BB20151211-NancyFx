using System.Data.Entity;
using Common.DataModels;
using Common.Interfaces;

namespace Data.Context
{
    public class DataContext : DbContext
    {
        public IDbSet<Room> Rooms { get; set; }
        public IDbSet<User> Users { get; set; }

        public DataContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DataContext>());
        }
    }
}