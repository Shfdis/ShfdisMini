using System.ComponentModel.DataAnnotations;

namespace LoginHandler;

public class LoginHandler : IDisposable, IAsyncDisposable
{
    private readonly SessionContext? _sessionContext = new();

    public IPasswordHashing GetLoginChecker(string userId)
    {
        try
        {
            IEnumerable<PasswordHashing> hashings = from passwordHasing
                    in _sessionContext.PasswordHashings
                    where passwordHasing.UserId == userId
                    select passwordHasing;
            return hashings.Single();
        }
        catch (Exception)
        {
            throw new ArgumentException("No such user");
        }
    }
    
    public void AddUser(string userId, string password)
    {
        _sessionContext.PasswordHashings.Add(new PasswordHashing(userId, password));
        _sessionContext.SaveChanges();
    }

    public void RemoveUser(string userId, string password)
    {
        if (!GetLoginChecker(userId).VerifyPassword(password))
        {
            throw new ValidationException("Unauthorized user");
        }
        _sessionContext.Remove(_sessionContext.PasswordHashings.Single(h => h.UserId == userId));
        _sessionContext.SaveChanges();
    }
    public void Dispose()
    {
        _sessionContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_sessionContext != null)
        {
            await _sessionContext.DisposeAsync();
        }
    }
}