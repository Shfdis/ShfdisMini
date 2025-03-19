using System.Data;
using System.Security.Authentication;
using LoginHandler;

namespace SessionHandler;

public sealed class SessionManager : IDisposable, IAsyncDisposable
{
    private readonly SessionContext _sessionContext;
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
                _sessionContext.Sessions.Add(session);
                _sessionContext.SaveChanges();
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

    public bool IsActive(string token)
    {
        IEnumerable<Session> sessions = from p 
            in _sessionContext.Sessions
            where p.SessionToken == token
                select p;
        return sessions.Any();
    }

    public void EndSession(string sessionToken)
    {
        try
        {
            ISession session =
                (from p in _sessionContext.Sessions 
                    where p.SessionToken == sessionToken
                    select p).Single();
            _sessionContext.Remove(session);
            _sessionContext.SaveChanges();
        }
        catch
        {
            throw new DataException("Session is inactive.");
        }
    }
    public void Dispose()
    {
        _sessionContext?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _sessionContext.DisposeAsync();
    }
}