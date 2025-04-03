namespace TestLoginManager;
using LoginHandler;
public class TestsForLogin
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    [Category("Add/Remove user")]
    public void Test1()
    {
        using (ILoginManager loginManager =  LoginManagerFactory.CreateLoginManager())
        {
            loginManager.AddUser("shfdis", "5456");
        }

        using (ILoginManager loginManager = LoginManagerFactory.CreateLoginManager())
        {
            loginManager.RemoveUser("shfdis", "5456");
        }
        
    }
    [Test]
    [Category("Login")]
    public void Test2()
    {
        using ILoginManager loginManager = LoginManagerFactory.CreateLoginManager();
        loginManager.AddUser("shfdis", "5456");
        IPasswordHashing checker = loginManager.GetLoginChecker("shfdis");
        
        Assert.True(checker.VerifyPassword("5456"));
        Assert.False(checker.VerifyPassword("5454"));
        Assert.False(checker.VerifyPassword("546"));
        
        loginManager.RemoveUser("shfdis", "5456");
    }
}