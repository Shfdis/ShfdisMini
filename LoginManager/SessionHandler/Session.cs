using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
namespace SessionHandler;
[PrimaryKey(nameof(Id))]
internal sealed class Session : ISession
{
    static string RandomString(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        string res = "";
        using RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] uintBuffer = new byte[sizeof(uint)];
        while (length-- > 0)
        {
            rng.GetBytes(uintBuffer);
            uint num = BitConverter.ToUInt32(uintBuffer, 0);
            res += valid[(int)(num % (uint)valid.Length)];
        }
        return res;
    }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    internal int Id { get; set; }
    [Key]
    public string UserId { get; set; }
    [Key]
    public string SessionToken { get; set; } = RandomString(50);
    
    [Column(TypeName="Date")]
    public DateTime CreatedDate { get; } = DateTime.Now;
}