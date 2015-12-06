using System.Data.Entity;

namespace Data.Context
{
    public class DataInitializer : CreateDatabaseIfNotExists<DataContext>
    {
    }
}