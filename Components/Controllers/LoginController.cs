using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Christmas.Components.Services;

namespace Christmas.Components.Controllers;

[AllowAnonymous]
public class LoginController(MongoDbUserService mongoDbUserService, LoggerFactory loggerFactory) : Controller
{
    [Route("api/login")]
    [HttpGet]
    public IActionResult Login([FromQuery] string codename)
    {
        var loginLogger = loggerFactory.CreateLogger("Login");
        loginLogger.LogInformation($"Attempted login with codename {codename}");
        string authenticatedUsername;
        
        try
        {
            authenticatedUsername = mongoDbUserService.GetUsername(codename.ToLower().Trim());
        }
        catch (InvalidOperationException exception)
        {
            return Redirect("/?FailedLogin=true");
        }
        
        var identity = new ClaimsIdentity("Cookies");
        identity.AddClaim(new("name", authenticatedUsername));

        var claimsPrincipal = new ClaimsPrincipal(identity);

        var authProps = new AuthenticationProperties
        {
            RedirectUri = "/presents"
        };

        return SignIn(claimsPrincipal, authProps, "Cookies");
    }

    [Route("api/logout")]
    [HttpGet]
    public IActionResult Logout()
    {
        var authProps = new AuthenticationProperties
        {
            RedirectUri = "/"
        };

        return SignOut(authProps, "Cookies");
    }
}
