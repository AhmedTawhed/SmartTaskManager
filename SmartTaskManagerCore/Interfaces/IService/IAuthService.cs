using Microsoft.AspNetCore.Identity;
using SmartTaskManager.Core.ViewModel;
using System.Collections;

namespace SmartTaskManager.Core.Interfaces.IService
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(RegisterViewModel model);
        Task<SignInResult> Login(LoginViewModel model);
        Task Logout();
    }
}