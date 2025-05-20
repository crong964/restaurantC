using _netmvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace _netmvc.Controllers;

public class LoginController : Controller
{
    [BindProperty]
    public required Login Login { get; set; }
    public IActionResult Index()
    {

        return View(Login);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignUp()
    {
        if (ModelState.IsValid)
        {
            // Use Input.Email and Input.Password to authenticate the user
            // with your custom authentication logic.
            //
            // For demonstration purposes, the sample validates the user
            // on the email address maria.rodriguez@contoso.com with 
            // any password that passes model validation.


            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Login.acc),
            new Claim("FullName", Login.pw),
            new Claim(ClaimTypes.Role, "A"),
        };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = "/Login"
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(10)

            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        // Something failed. Redisplay the form.

        return View(Login);
    }


    public async Task<IActionResult> Signout()
    {
        await HttpContext.SignOutAsync(
       CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index");
    }

    

}