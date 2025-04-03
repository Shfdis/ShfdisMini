using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace DbSessionCreator;
using Microsoft.EntityFrameworkCore;

public class SessionContext : DbContext
{
    protected SessionContext()
    {
        try
        {
            var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            databaseCreator.CreateTables();
        }
        catch
        {
            //A SqlException will be thrown if tables already exist. So simply ignore it.
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder.UseNpgsql($"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                                 $"Port=5433;" +
                                 $"Database=shfdismini;" +
                                 $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                                 $"Password={Environment.GetEnvironmentVariable("DB")}");
        optionsBuilder.LogTo(System.Console.WriteLine);
    }
}