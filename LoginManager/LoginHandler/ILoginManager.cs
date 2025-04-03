namespace LoginHandler;

public interface ILoginManager : IDisposable
{
    public void RemoveUser(string userId, string password);
    public void AddUser(string userId, string password);
    public IPasswordHashing GetLoginChecker(string userId);
    public bool Any(string userId);
}