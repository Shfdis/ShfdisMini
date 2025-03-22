using Microsoft.EntityFrameworkCore;


namespace LoginHandler;

internal class SessionContext : DbSessionCreator.SessionContext
{
    public DbSet<PasswordHashing> PasswordHashings { get; set; }
}