namespace SessionHandler;

public interface ISessionManager : IDisposable
{
    public ISession CreateUserSession(string userId, string password);
    public bool IsActive(string token);
    public void EndSession(string sessionToken);
}