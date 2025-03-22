namespace LoginHandler;

public static class LoginManagerFactory
{
    public static ILoginManager CreateLoginManager()
    {
        return new LoginManager();
    }
}