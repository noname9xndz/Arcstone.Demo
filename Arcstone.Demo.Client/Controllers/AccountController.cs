using Arcstone.Demo.Client.Heplers;
using Arcstone.Demo.Client.Models;
using Arcstone.Demo.Client.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Arcstone.Demo.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserClientService _userClientService;
        private readonly ConfigAudience _appSettings;

        public AccountController(IUserClientService userClientService, IOptions<ConfigAudience> appSettings)
        {
            _userClientService = userClientService;
            _appSettings = appSettings.Value;
        }

        public async Task<IActionResult> Login()
        {
            LoginModel login = new LoginModel();
            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                var token = await _userClientService.GetToken(login);
                if (token.ErrorMessage != null && !token.ErrorMessage.Any() && !string.IsNullOrWhiteSpace(token.Result))
                {
                    var jwToken = token.Result.GetPrincipalFromExpiredToken(_appSettings);

                    if (jwToken != null)
                    {
                        long expiresTime = DateTime.Now.AddHours(1).Hour;
                        var expiresToken = jwToken.Claims.FirstOrDefault(x => x.Type.Contains("exp"))?.Value;
                        if (expiresToken != null)
                        {
                            if (long.TryParse(expiresToken, out var time))
                            {
                                expiresTime = time;
                            }
                        }
                        var claimsIdentity = new ClaimsIdentity(
                            jwToken.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            //AllowRefresh = true,
                            ExpiresUtc = DateTimeOffset.FromUnixTimeSeconds(expiresTime),
                            IsPersistent = true,
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(AccountController.Login), "Account");
        }
    }
}