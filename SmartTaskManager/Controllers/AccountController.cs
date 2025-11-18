using Microsoft.AspNetCore.Mvc;
using SmartTaskManagerCore.Core.Interfaces.IService;
using SmartTaskManagerCore.ViewModel;

namespace SmartTaskManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _auth;

        public AccountController(IAuthService auth)
        {
            _auth = auth;
        }
        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _auth.Register(model);

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
                var result = await _auth.Login(model);
                if (result.Succeeded) return RedirectToAction("Index", "Home");

                ModelState.AddModelError("", "Ivalid login attempt");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _auth.Logout();
            return RedirectToAction("Login");
        }
    }
}