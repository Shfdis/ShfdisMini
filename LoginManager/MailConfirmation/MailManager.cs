using DbSessionCreator;
using Microsoft.EntityFrameworkCore;

namespace MailConfirmation;

internal class MailManager : SessionContext, IMailManager
{
    DbSet<MailConfirmation> MailConfirmations { get; set; }

    public bool IsConfirmed(string email)
    {
        return MailConfirmations.Any(x => x.Email == email && x.Confirmed);
    }
    
    
}