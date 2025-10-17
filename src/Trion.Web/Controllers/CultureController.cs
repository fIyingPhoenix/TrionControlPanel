using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CultureController : Controller
{
    [HttpGet("Set")]
    public IActionResult Set(string culture = "en", string? redirectUri = "/")
    {
        var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture));
        Response.Cookies.Append("trion_lang", cookieValue, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddYears(1),
            Path = "/",
            IsEssential = true,
            HttpOnly = false,
            SameSite = SameSiteMode.Lax
        });

        // ensure redirect is local
        if (string.IsNullOrWhiteSpace(redirectUri)) redirectUri = "/";
        if (!Url.IsLocalUrl(redirectUri))
        {
            try { redirectUri = new Uri(redirectUri).PathAndQuery; }
            catch { redirectUri = "/"; }
        }

        return LocalRedirect(redirectUri);
    }
}
