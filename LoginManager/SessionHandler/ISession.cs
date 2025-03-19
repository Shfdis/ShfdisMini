namespace SessionHandler;

public interface ISession
{
    public string UserId { get; set; }
    public bool IsAuthentified(string token);

}