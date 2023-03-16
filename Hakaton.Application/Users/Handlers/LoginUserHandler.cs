using Hakaton.Application.Users.Request;
using Hakaton.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hakaton.Application.Common.Exceptions;

namespace Hakaton.Application.Users.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, LoginResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserStore<User> _userStore;
        private readonly IUserEmailStore<User> _emailStore;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AuthManager> _logger;

        public LoginUserHandler(
            UserManager<User> userManager,
            IUserStore<User> userStore,
            SignInManager<User> signInManager,
            ILogger<AuthManager> logger)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<User>)_userStore;
            _signInManager = signInManager;
            _logger = logger;
        }
        public async Task<LoginResult> Handle(LoginUserRequest request, CancellationToken token)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogInformation("User not found");
                throw new DException("User not found");
            }

            var result = await _signInManager.PasswordSignInAsync(request.Email,
     request.Password, false, false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return new LoginResult { Success = true };
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                throw new DException("User account locked out");
            }
            else
            {
                return new LoginResult { Success = false };
                throw new DException("Login Error");
            }
        }
    }
}
