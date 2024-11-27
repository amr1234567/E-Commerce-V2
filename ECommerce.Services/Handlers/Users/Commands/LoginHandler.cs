using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Base;
using ECommerce.Services.Abstractions;
using ECommerce.Services.CQRS;
using ECommerce.Services.Models.Inputs;
using ECommerce.Services.Models.Outputs;
using ECommerce.Services.Models.Outputs.Base;
using ECommerce.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Handlers.Users.Commands
{
    public class LoginHandler
        (IUserRepository userRepository,
        ITokenServices tokenServices,
        AccountServicesHelpers accountHelper)
        : ICommandHandler<LoginCommand, CustomResponse<TokenModel>>
    {
        public async Task<CustomResponse<TokenModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (request == null) return CustomResponse<TokenModel>.BadRequest("Argument is null");
            var user = await userRepository.GetUserByEmail(request.Email);
            if (!IsPasswordCorrect(request.Password, user.Password, user.Salt))
                return CustomResponse<TokenModel>.UnUnAuthorizedAccess("email or password wrong");

            var tokenModel = await GetTokenModelForUser(user);

            return CustomResponse<TokenModel>.Succeeded(tokenModel);
        }

        private bool IsPasswordCorrect(string password, string hashedPassword,string salt)
        {
            var passwordHash = accountHelper.HashPasswordWithSalt(salt, password);
            return passwordHash == hashedPassword;
        }

        private async Task<TokenModel> GetTokenModelForUser(IdentityBase user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Role, user.Role.ToString()),
            };
            var tokenModel = await tokenServices.GenerateNewTokenModel(user.Id, claims);
            return tokenModel;
        }
    }
}
