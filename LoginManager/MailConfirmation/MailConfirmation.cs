using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace MailConfirmation;
[PrimaryKey("Email")]
[Table("mails_confirmed")]
public class MailConfirmation
{
    [Column ("email")]
    public string Email { get; set; }
    [Column("confirmed")]
    public bool Confirmed { get; set; }
    [Column ("confirmation_code")]
    public string ConfirmationCode { get; set; }
}