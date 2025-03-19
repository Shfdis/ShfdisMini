namespace SessionHandler;

public interface ISession
{
    public string UserId { get; set; }
    public string SessionToken { get; }
    public DateTime CreatedDate{ get; }
}