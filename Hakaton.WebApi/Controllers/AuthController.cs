using Hakaton.Application.Users.Request;
using Hakaton.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hakaton.WebApi.Controllers
{
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly AuthManager _authManager;
        public AuthController(IMediator mediator, AuthManager authManager)
        {
            _mediator = mediator;
            _authManager = authManager;
        }

        [HttpPost("register")]
        public async Task Register(RegisterUserRequest request)
        {
            await _mediator.Send(request);
        }

        [HttpPost("login")]
        public async Task Login(LoginUserRequest request)
        {
            await _mediator.Send(request);
        }
    }
}
