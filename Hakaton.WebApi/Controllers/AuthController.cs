using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Hakaton.WebApi.Models;

namespace SimpleAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AuthController(UserManager<IdentityUser<Guid>> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<Guid> Register(RegisterDto register)
        {
            var existingUser = await _userManager.FindByNameAsync(register.UserName);
            if (existingUser != null)
            {
                throw new Exception("User with this username already exists.");
            }
            if (register.Password != register.ConfirmPasseord)
            {
                throw new Exception("Password is miss macthing!");
            }

            var user = new IdentityUser<Guid>()
            {
                Id = Guid.NewGuid(),
                UserName = register.UserName,
                Email = register.Email,
            };

            var identityResult = await _userManager.CreateAsync(user);

            if (!identityResult.Succeeded)
            {
                throw new Exception("Use upper case!");
            }

            identityResult = await _userManager.AddPasswordAsync(user, register.Password);

            if (!identityResult.Succeeded)
            {
                throw new Exception("Use special symbol, number! MinLength=6!");
            }

            return user.Id;
        }

        [HttpPost("/role/set")]
        public async Task SetRole(Guid userId, Guid roleId)
        {

            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role is null)
            {
                throw new Exception();
            }
            
            var user = await _userManager.FindByIdAsync(userId.ToString());          
            if (user is null)
            {
                throw new Exception();
            }

            var identityResult = await _userManager.AddToRoleAsync(user, role.Name);

            if (!identityResult.Succeeded)
            {
                throw new Exception();
            }
        }

        [HttpPost("/role/")]
        [AllowAnonymous]
        public async Task<Guid> AddRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new Exception();
            }

            if (await _roleManager.RoleExistsAsync(roleName))
            {
                throw new Exception();
            }

            var role = new IdentityRole<Guid>(roleName) { Id = Guid.NewGuid() };

            await _roleManager.CreateAsync(role);

            return role.Id;
        }

        [HttpPost("login")]
        public async Task Login(LoginDto login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null)
            {
                throw new Exception("User is not found ");
            }

            var passwordCorrect = await _userManager.CheckPasswordAsync(user, login.Password);

            if (!passwordCorrect)
            {
                throw new Exception("Password is not correct");
            }

            var identity = await GetIdentityAsync(user);

            await Request.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties()
            {
                IsPersistent = login.RememberMe
            });
        }
        [HttpPost("logout")]
        [Authorize]
        public async Task Logout()
        { 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private async Task<ClaimsIdentity> GetIdentityAsync(IdentityUser<Guid> user)
        {
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())};
            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(
                roles.Select(role => (
                    new Claim(ClaimTypes.Role, role))));

            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
