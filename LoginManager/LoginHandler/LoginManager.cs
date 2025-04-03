using System.ComponentModel.DataAnnotations;

namespace LoginHandler;

internal class LoginManager : SessionContext, ILoginManager
{

    public IPasswordHashing GetLoginChecker(string userId)
    {
        try
        {
            IEnumerable<PasswordHashing> hashings = from passwordHasing
                    in PasswordHashings
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
        PasswordHashings.Add(new PasswordHashing(userId, password));
        SaveChanges();
    }

    public void RemoveUser(string userId, string password)
    {
        if (!GetLoginChecker(userId).VerifyPassword(password))
        {
            throw new ValidationException("Unauthorized user");
        }
        Remove(PasswordHashings.Single(h => h.UserId == userId));
        SaveChanges();
    }

    public bool Any(string userId)
    {
        return (from p in PasswordHashings select p.UserId == userId).Any();
    }
}