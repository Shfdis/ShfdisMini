using DbSessionCreator;
using Microsoft.EntityFrameworkCore;

namespace MailConfirmation;

internal class MailManager : SessionContext, IMailManager
{
    DbSet<MailConfirmation> MailConfirmations { get; set; }

    public bool IsConfirmed(string email)
    {
        return (from p in MailConfirmations where p.Email == email && p.Confirmed select p).Count() > 0;
    }
    
    
}