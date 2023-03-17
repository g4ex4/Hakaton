using System.ComponentModel.DataAnnotations;

namespace Hakaton.WebApi.Models
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPasseord { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
