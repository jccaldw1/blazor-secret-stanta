using Christmas.Components;
using Christmas.Components.MongoDb.Contexts;
using Christmas.Components.Services;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_MONGODB_URI");

if (connectionString == null)
{
    Console.WriteLine("You must set your 'MONGODB_URI' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
    Environment.Exit(0);
}

var builder = WebApplication.CreateBuilder(args);
connectionString = builder.Configuration["MONGODB_URI"];

//builder.Configuration.AddEnvironmentVariables("ASPNETCORE_");
Console.WriteLine($"connectionstring: {connectionString}");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddControllers();
builder.Services.AddDbContextFactory<UsersContext>(options =>
{
    options.UseMongoDB(new MongoClient(builder.Configuration["MONGODB_URI"] ?? throw new Exception("can't find your stupid connectionstring")), "Users");
});

builder.Services.AddTransient<MongoDbUserService>();
builder.Services.AddTransient<ChristmasPresentService>();

var authenticationBuilder = builder.Services.AddAuthentication("Cookies");
authenticationBuilder.AddCookie("Cookies");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapControllers();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
