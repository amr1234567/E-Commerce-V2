using ECommerce.Domain.Enums;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Services.Exceptions
{
    [Serializable]
    public class TokenNotValidException : BaseException
    {
        public TokenNotValidException(string? message) : base(FailureType.UnAuthorized, message)
        {
        }

    }
}