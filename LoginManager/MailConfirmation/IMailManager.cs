namespace MailConfirmation;

public interface IMailManager : IDisposable
{
    public bool IsConfirmed(string email);
}