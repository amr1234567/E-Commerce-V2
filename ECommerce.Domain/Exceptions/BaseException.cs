using ECommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Exceptions
{
    public class BaseException : Exception
    {
        public FailureType FailureType { get; private set; }

        public BaseException(FailureType failureType, string? message) : base(message)
        {
            FailureType = failureType;
        }
    }
}
