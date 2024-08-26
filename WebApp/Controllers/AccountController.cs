using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Tentative de connexion invalide.");
                return View(model);
            }

            // Appeler la méthode RateLimiter
            if (RateLimiter(user))
            {
                ModelState.AddModelError(string.Empty, "Compte verrouillé. Veuillez réessayer plus tard.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Réinitialiser le compteur de tentatives échouées
                user.AccessFailedCount = 0;

                // Vérifier si c'est la première connexion
                if (user.IsFirstLogin)
                {
                    return RedirectToAction("ChangePassword");
                }

                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "Home");
            }

            // Si l'authentification échoue, gérer les tentatives échouées et verrouiller si nécessaire
            await HandleFailedLogin(user);

            ModelState.AddModelError(string.Empty, "Tentative de connexion invalide.");
            return View(model);
        }

        private bool RateLimiter(ApplicationUser user)
        {
            var rateLimiterEnabled = bool.Parse(_configuration["RateLimiter:Enabled"]);
            if (rateLimiterEnabled && user.LockoutEnd.HasValue && user.LockoutEnd > DateTimeOffset.UtcNow)
            {
                return true;
            }
            return false;
        }

        private async Task HandleFailedLogin(ApplicationUser user)
        {
            var rateLimiterEnabled = bool.Parse(_configuration["RateLimiter:Enabled"]);
            var maxFailedAttempts = int.Parse(_configuration["RateLimiter:MaxFailedAttempts"]);
            var lockoutDuration = TimeSpan.Parse(_configuration["RateLimiter:LockoutDuration"]);

            if (rateLimiterEnabled)
            {
                user.AccessFailedCount++;
                if (user.AccessFailedCount >= maxFailedAttempts)
                {
                    user.LockoutEnd = DateTimeOffset.UtcNow.Add(lockoutDuration);
                    ModelState.AddModelError(string.Empty, $"Compte verrouillé pour {lockoutDuration.TotalSeconds} secondes en raison de plusieurs tentatives de connexion échouées.");
                }

                await _userManager.UpdateAsync(user);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                user.IsFirstLogin = false;
                await _userManager.UpdateAsync(user);

                TempData["SuccessMessage"] = "Votre mot de passe a été changé avec succès.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}
