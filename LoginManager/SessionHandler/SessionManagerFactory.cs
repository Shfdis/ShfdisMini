namespace SessionHandler;

public static class SessionManagerFactory
{
    public static ISessionManager CreateSession()
    {
        return new SessionManager();
    }

    public static Type GetSessionManagerType()
    {
        return typeof(SessionManager);
    }
}