using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using GhostDrive.Application.Users.Commands.Login;
using GhostDrive.Application.Users.Commands.Register;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GhostDrive.Web.Controllers.UI
{
    public class AccountController : BaseController
    {
        private readonly IStringLocalizer<AccountController> localizer;

        public AccountController(IStringLocalizer<AccountController> localizer)
        {
            this.localizer = localizer;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await Mediator.Send(command);
                if (result.IsSuccess)
                {
                    await Authenticate(command.Login);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, localizer[result.FailureReason]);
            }
            return View(command);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await Mediator.Send(command);
                if (result.IsSuccess)
                {
                    await Authenticate(command.Login);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, localizer[result.FailureReason]);
            }
            return View(command);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private async Task Authenticate(string login)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login)
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}