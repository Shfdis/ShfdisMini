namespace TestLoginManager;
using LoginHandler;
using SessionHandler;
public class TestsForSession
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    [Category("Session creation/deletion")]
    public void Test1()
    {
        using (ILoginManager loginManager = LoginManagerFactory.CreateLoginManager())
        {
            loginManager.AddUser("shfdis", "5456");
            loginManager.AddUser("yunetive", "alan");
        }
        string shfdisToken, yunetiveToken;
        using (ISessionManager sessionHandler = SessionManagerFactory.CreateSession())
        {
            ISession shfdisSession = sessionHandler.CreateUserSession("shfdis", "5456");
            Assert.True(sessionHandler.IsActive(shfdisSession.SessionToken));
            shfdisToken = shfdisSession.SessionToken;
            ISession yunetiveSession = sessionHandler.CreateUserSession("yunetive", "alan");
            Assert.True(sessionHandler.IsActive(yunetiveSession.SessionToken));
            yunetiveToken = yunetiveSession.SessionToken;
        }

        using (ISessionManager sessionManager = SessionManagerFactory.CreateSession())
        {
            sessionManager.EndSession(shfdisToken);
            sessionManager.EndSession(yunetiveToken);
        }

        using (ILoginManager loginManager = LoginManagerFactory.CreateLoginManager())
        {
            loginManager.RemoveUser("shfdis", "5456");
            loginManager.RemoveUser("yunetive", "alan");
        }
    }
   
}