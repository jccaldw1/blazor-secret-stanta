using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Christmas.Components.Services;

namespace Christmas.Components.Controllers;

[AllowAnonymous]
public class LoginController(MongoDbUserService mongoDbUserService) : Controller
{
    [Route("api/login")]
    [HttpGet]
    public IActionResult Login([FromQuery] string username)
    {
        if (mongoDbUserService.IsValidUser(username))
        {
            var identity = new ClaimsIdentity("Cookies");
            identity.AddClaim(new("name", username));

            var claimsPrincipal = new ClaimsPrincipal(identity);

            var authProps = new AuthenticationProperties
            {
                RedirectUri = "/"
            };

            return SignIn(claimsPrincipal, authProps, "Cookies");
        }

        return Redirect("/login?FailedLogin=true");
    }

    [Route("api/logout")]
    [HttpGet]
    public IActionResult Logout()
    {
        var authProps = new AuthenticationProperties
        {
            RedirectUri = "/logout"
        };

        return SignOut(authProps, "Cookies");
    }
}
