using Microsoft.AspNetCore.Identity;
using SmartTaskManagerCore.ViewModel;
using System.Collections;

namespace SmartTaskManagerCore.Core.Interfaces.IService
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(RegisterViewModel model);
        Task<SignInResult> Login(LoginViewModel model);
        Task Logout();
    }
}