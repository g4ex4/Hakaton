using MediatR;
using System.ComponentModel.DataAnnotations;


namespace Hakaton.Application.Users.Request
{
    public class RegisterUserRequest : IRequest<RegisterResult>
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
