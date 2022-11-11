using AutoMapper;
using IdentityServer.Application.Interfaces;
using IdentityServer.Application.Models;
using IdentityServer.Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AuthService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> LoginAsync(LoginModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, false);

            return result.Succeeded;
        }

        public async Task<bool> RegisterAsync(RegisterModel registerModel)
        {
            var user = _mapper.Map<User>(registerModel);

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            return result.Succeeded;
        }
    }
}
