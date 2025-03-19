using System.Data;
using System.Security.Authentication;
using LoginHandler;

namespace SessionHandler;

public sealed class SessionManager : IDisposable, IAsyncDisposable
{
    private SessionContext? _sessionContext = null;
    public SessionManager()
    {
        try
        {
            _sessionContext = new SessionContext();
        }
        catch (Exception)
        {
            throw new DataException("Could not create Session Context.");
        }
    }
    public ISession CreateUserSession(string userId, string password)
    {
        try
        {
            using LoginHandler.LoginHandler handler = new LoginHandler.LoginHandler();
            IPasswordHashing passwordHashing = handler.GetLoginChecker(userId);
            if (passwordHashing.VerifyPassword(password))
            {
                Session session = new Session { UserId = userId };
                _sessionContext?.Sessions.Add(session);
                _sessionContext?.SaveChanges();
                return session;
            }
            else
            {
                throw new AuthenticationException("Invalid password.");
            }
        }
        catch (Exception)
        {
            throw new DataException("Could not create user Session.");
        }
    }
    
    public void Dispose()
    {
        _sessionContext?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_sessionContext != null)
        {
            await _sessionContext.DisposeAsync();
        }
    }
}