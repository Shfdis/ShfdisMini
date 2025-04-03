namespace MailConfirmation;

public static class MailManagerFactory
{
    public static IMailManager CreateMailManager()
    {
        return new MailManager();
    }
}