using Microsoft.AspNetCore.Identity;
using SmartTaskManager.Core.Entities;
using SmartTaskManager.Core.Interfaces.IService;
using SmartTaskManager.Core.ViewModel;

namespace SmartTaskManager.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<SignInResult> Login(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
        }

        public async Task Logout() => await _signInManager.SignOutAsync();


        public async Task<IdentityResult> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user,model.Password);

            if(result.Succeeded) await _signInManager.SignInAsync(user, isPersistent: false);

            return result;
        }
    }
}