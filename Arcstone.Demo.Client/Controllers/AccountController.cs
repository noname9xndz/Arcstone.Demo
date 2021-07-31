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
using Arcstone.Demo.Application.Domain.Project.Queries;

namespace Arcstone.Demo.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserClientService _userClientService;
        private readonly IProjectClientService _projectClientService;
        private readonly ConfigAudience _appSettings;

        public AccountController(IUserClientService userClientService, IOptions<ConfigAudience> appSettings, IProjectClientService projectClientService)
        {
            _userClientService = userClientService;
            _projectClientService = projectClientService;
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
                        var claims =  jwToken.Claims.ToList();
                        claims.Add(new Claim("JwtToken", $"{token.Result}"));
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
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);
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

        public async Task<IActionResult> Test()
        {
            var request = new GetAllProjectQuery()
            {
                PageIndex = 1,
                PageSize = 10,
                StartTime = 1627142405,
                EndTime = 1627660805
            };
            var models = await _projectClientService.GetProjectsPaging(request);
            return View();
        }
    }
}