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
            res.Append(valid[(int)(num % (uint)valid.Length)]);
        }

        return res;
    }
    internal string Id { get; set; }
    public string UserId { get; set; }
    private string SessionToken { get; set; } = RandomString(50);
    public bool IsAuthentified(string token)
    {
        return SessionToken == token;
    }
}