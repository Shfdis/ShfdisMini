namespace LoginHandler;

public interface IPasswordHashing
{
    public bool VerifyPassword(string password);
}