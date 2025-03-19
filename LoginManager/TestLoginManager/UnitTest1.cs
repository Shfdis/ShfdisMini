namespace TestLoginManager;
using LoginHandler;
public class Tests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    [Category("Add/Remove user")]
    public void Test1()
    {
        using (LoginHandler loginHandler = new LoginHandler())
        {
            loginHandler.AddUser("shfdis", "5456");
        }

        using (LoginHandler loginHandler = new LoginHandler())
        {
            loginHandler.RemoveUser("shfdis", "5456");
        }
        
    }
    [Test]
    [Category("Login")]
    public void Test2()
    {
        using LoginHandler loginHandler = new LoginHandler();
        loginHandler.AddUser("shfdis", "5456");
        IPasswordHashing checker = loginHandler.GetLoginChecker("shfdis");
        
        Assert.True(checker.VerifyPassword("5456"));
        Assert.False(checker.VerifyPassword("5454"));
        Assert.False(checker.VerifyPassword("546"));
        
        loginHandler.RemoveUser("shfdis", "5456");
    }
}