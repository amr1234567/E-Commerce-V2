using ECommerce.Services.CQRS;
using ECommerce.Services.Models.Outputs;
using ECommerce.Services.Models.Outputs.Base;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Services.Models.Inputs
{
    public class LoginCommand : ICommand<CustomResponse<TokenModel>>
    {
        [Required]
        [EmailAddress]
        public string Email { get; private set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; private set; }

        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
