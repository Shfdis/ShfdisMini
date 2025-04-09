using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace LoginHandler;
[PrimaryKey("UserId")]
[Table("password_hashings")]
internal class PasswordHashing : IPasswordHashing
{
    public PasswordHashing()
    {
        // pass
    }
    [Column("user_id")]
    public string UserId { get; set; }
    [Column("password_hash")]
    public string PasswordHash { get; set; }

    public PasswordHashing(string userId, string password)
    {
        UserId = userId;
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);
        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);
        PasswordHash = Convert.ToBase64String(hashBytes);
    }

    public bool VerifyPassword(string password)
    {
        byte[] hashBytes = Convert.FromBase64String(PasswordHash);
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);
        for (int i = 0; i < 20; i++)
        {
            if (hashBytes[i + 16] != hash[i])
            {
                return false;
            }
        }

        return true;
    }
}