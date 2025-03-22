using System.Data;
using System.Security.Authentication;
using LoginHandler;

namespace SessionHandler;

internal sealed class SessionManager : SessionContext, ISessionManager
{
    
    public ISession CreateUserSession(string userId, string password)
    {
        try
        {
            using ILoginManager manager = LoginManagerFactory.CreateLoginManager();
            IPasswordHashing passwordHashing = manager.GetLoginChecker(userId);
            if (passwordHashing.VerifyPassword(password))
            {
                Session session = new Session { UserId = userId };
                Sessions.Add(session);
                SaveChanges();
                return session;
            }

            throw new AuthenticationException("Invalid password.");
        }
        catch (AuthenticationException e)
        {
            throw e;
        }
        catch (Exception)
        {
            throw new DataException("Could not create user Session.");
        }
    }

    public bool IsActive(string token)
    {
        IEnumerable<Session> sessions = from p 
            in Sessions
            where p.SessionToken == token
                select p;
        return sessions.Any();
    }

    public void EndSession(string sessionToken)
    {
        try
        {
            ISession session =
                (from p in Sessions 
                    where p.SessionToken == sessionToken
                    select p).Single();
            Remove(session);
            SaveChanges();
        }
        catch
        {
            throw new DataException("Session is inactive.");
        }
    }
}