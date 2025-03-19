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
        using (LoginHandler loginHandler = new LoginHandler())
        {
            loginHandler.AddUser("shfdis", "5456");
            loginHandler.AddUser("yunetive", "alan");
        }
        string shfdisToken, yunetiveToken;
        using (SessionManager sessionHandler = new())
        {
            ISession shfdisSession = sessionHandler.CreateUserSession("shfdis", "5456");
            Assert.True(sessionHandler.IsActive(shfdisSession.SessionToken));
            shfdisToken = shfdisSession.SessionToken;
            ISession yunetiveSession = sessionHandler.CreateUserSession("yunetive", "alan");
            Assert.True(sessionHandler.IsActive(yunetiveSession.SessionToken));
            yunetiveToken = yunetiveSession.SessionToken;
        }

        using (SessionManager sessionManager = new())
        {
            sessionManager.EndSession(shfdisToken);
            sessionManager.EndSession(yunetiveToken);
        }

        using (LoginHandler loginHandler = new())
        {
            loginHandler.RemoveUser("shfdis", "5456");
            loginHandler.RemoveUser("yunetive", "alan");
        }
    }
   
}