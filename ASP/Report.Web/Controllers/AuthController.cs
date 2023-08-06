using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Report.Domain.Models;

namespace Report.Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    public IActionResult Index(string returnUrl = "/")
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string login, string password, string returnUrl = "/")
    {
        var userResult = await _authService.GetUserAsync(login, password);

        if (userResult is not OkResult<User> user) return RedirectToAction("Index");
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Result.Name),
            new(ClaimTypes.NameIdentifier, user.Result.Id.ToString()),
            new(ClaimTypes.Role, "")
        };

        var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));


        // var claimsIdentity = new ClaimsIdentity(
        //     claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //
        // var authProperties = new AuthenticationProperties
        // {
        //     AllowRefresh = true,// Refreshing the authentication session should be allowed.
        //     IsPersistent = true,
        //     IssuedUtc = DateTime.UtcNow,
        // };

        // await HttpContext.SignInAsync(
        //     CookieAuthenticationDefaults.AuthenticationScheme, 
        //     new ClaimsPrincipal(claimsIdentity), 
        //     authProperties);
        return Redirect(returnUrl);
    }

    [Authorize]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }

    public async Task<IActionResult> Register(string login, string password)
    {
        await _authService.RegisterAsync(login, password);
        return Redirect("/");
    }
}