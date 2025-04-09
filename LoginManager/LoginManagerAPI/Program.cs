using System.Web.Http;
using LoginHandler;
using LoginManagerAPI;
using SessionHandler;
using MailConfirmation;
using ISession = SessionHandler.ISession;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.MapGet("/", () => new {status = "success"});
app.MapPost("/signup", object ([Microsoft.AspNetCore.Mvc.FromBody]User user) =>
{
    using (ILoginManager mgr = LoginManagerFactory.CreateLoginManager())
    {
        using (IMailManager mailMgr = MailManagerFactory.CreateMailManager())
        {
            if (!mailMgr.IsConfirmed(user.UserId))
            {
                return new { status = "error", message = "mail is not confirmed" };
            }
        }
        if (mgr.Any(user.UserId))
        {
            return new { status = "error", message = "Account with this email already exists" };
        }

        try
        {
            mgr.AddUser(user.UserId, user.Password);
        }
        catch (Exception ex)
        {
            return new { status = "error", message = ex.Message };
        }
    }
    return new { status = "success" };
}).WithName("signup");
app.MapDelete("/delete", object ([Microsoft.AspNetCore.Mvc.FromBody]User user) => {
    try
    {
        using (ILoginManager mgr = LoginManagerFactory.CreateLoginManager())
        {
            mgr.RemoveUser(user.UserId, user.Password);
        }
        return new { status = "success" };
    }
    catch (Exception ex)
    {
        return new { status = "error", message = ex.Message };
    }
}).WithName("delete user");


app.MapGroup("/session");
app.MapPost("/session",
    object([Microsoft.AspNetCore.Mvc.FromBody]User user) =>
    {
        using ISessionManager handler = SessionManagerFactory.CreateSession();
        try
        {
            ISession answer = handler.CreateUserSession(user.UserId, user.Password);
            return new {status = "success", token=answer.SessionToken};
        }
        catch (Exception ex)
        {
            return new {status = ex.Message};
        }
    }).WithName("CreateSession");
app.MapGet("/session/{token}", ([FromUri]string token) =>
{
    using ISessionManager handler = SessionManagerFactory.CreateSession();
    return new {active = handler.IsActive(token), status = "success"};
});
app.MapDelete("/session/{token}",
    ([FromUri]string token) =>
    {
        using ISessionManager handler = SessionManagerFactory.CreateSession();
        try
        {
            handler.EndSession(token);
            return new {status = "success"};
        }
        catch (Exception ex)
        {
            return new {status = ex.Message};
        }
    }).WithName("EndSession");
app.Run();
