using Microsoft.EntityFrameworkCore;

namespace SessionHandler;

internal class SessionContext : DbSessionCreator.SessionContext
{
    public DbSet<Session> Sessions { get; set; }
}