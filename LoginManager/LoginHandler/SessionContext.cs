using Microsoft.EntityFrameworkCore;


namespace LoginHandler;

internal sealed class SessionContext : DbSessionCreator.SessionContext
{
    public DbSet<PasswordHashing> PasswordHashings { get; set; }
}