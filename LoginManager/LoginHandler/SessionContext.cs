using SessionHandler;

namespace LoginHandler;
using Microsoft.EntityFrameworkCore;

internal sealed class SessionContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=loginManager.db");
    }
    public DbSet<PasswordHashing> PasswordHashings { get; set; }
    public DbSet<Session> Sessions { get; set; }
}