using DbSessionCreator;
using Microsoft.EntityFrameworkCore;

namespace MailConfirmation;

public class MailManager : SessionContext
{
    DbSet<MailConfirmation> MailConfirmations { get; set; }
    
}