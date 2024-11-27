using ECommerce.Domain.Enums;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Services.Exceptions
{
    [Serializable]
    public class NotValidTokenSignatureException : BaseException
    {

        public NotValidTokenSignatureException(string? message) : base(FailureType.UnAuthorized, message)
        {
        }

    }
}