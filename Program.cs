using Azure.Identity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Azure;
using Christmas.Components;
using Christmas.Components.Container;
using Christmas.Components.MongoDb.Contexts;
using Christmas.Components.Services;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var BlobStorageUri = builder.Configuration["AzureURIs:BlobStorage"];
var KeyVaultURI = builder.Configuration["AzureURIs:KeyVault"];

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddControllers();
builder.Services.AddDbContextFactory<UsersContext>(options =>
{
    options.UseMongoDB(new MongoClient(builder.Configuration["mongodb-uri"] ?? throw new Exception("You must set your 'mongodb-uri' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string")), "Users");
});

builder.Services.AddTransient<MongoDbUserService>();
builder.Services.AddTransient<ChristmasPresentService>();
builder.Services.AddScoped<SelectedUserContainer>();



var authenticationBuilder = builder.Services.AddAuthentication("Cookies");
authenticationBuilder.AddCookie("Cookies");

builder.Services.AddAzureClientsCore();

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
