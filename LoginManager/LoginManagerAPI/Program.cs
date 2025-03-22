using LoginHandler;
using SessionHandler;
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
app.MapPost("/signup/{userId}/{password}", () => new { status = "not implemented" }).WithName("signup");
app.MapDelete("/delete/{userId}/{password}", object (string userId, string password) => {
    try
    {
        using (ILoginManager mgr = LoginManagerFactory.CreateLoginManager())
        {
            mgr.RemoveUser(userId, password);
        }
        return new { status = "success" };
    }
    catch (Exception ex)
    {
        return new { status = "error", message = ex.Message };
    }
}).WithName("delete user");


app.MapGroup("/session");
app.MapPost("/session/create/{userId}/{password}",
    object(string userId, string password) =>
    {
        using ISessionManager handler = SessionManagerFactory.CreateSession();
        try
        {
            ISession answer = handler.CreateUserSession(userId, password);
            return new {status = "success", token=answer.SessionToken};
        }
        catch (Exception ex)
        {
            return new {status = ex.Message};
        }
    }).WithName("CreateSession");
app.MapGet("/session/{token}", (string token) =>
{
    using ISessionManager handler = SessionManagerFactory.CreateSession();
    return new {active = handler.IsActive(token), status = "success"};
});
app.MapDelete("/session/end/{token}",
    (string token) =>
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
