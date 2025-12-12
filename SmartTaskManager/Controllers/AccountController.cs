using Microsoft.AspNetCore.Mvc;
using SmartTaskManager.Core.Interfaces.IService;
using SmartTaskManager.Core.ViewModel;

namespace SmartTaskManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Register(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var item in result.Errors)
                    ModelState.AddModelError("",item.Description);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Login(model);
                if (result.Succeeded) return RedirectToAction("Index", "Home");

                ModelState.AddModelError("", "Ivalid login attempt");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return RedirectToAction("Login");
        }
    }
}