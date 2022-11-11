using IdentityServer.Application.Models;

namespace IdentityServer.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginModel loginModel);
        Task<bool> RegisterAsync(RegisterModel registerModel);
    }
}
