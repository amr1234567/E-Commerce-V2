using ECommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Models.Outputs.Base
{
    public class CustomResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; } = default(T);
        public ErrorModel? Error { get; set; }

        private CustomResponse(){}

        public static CustomResponse<T> Succeeded(T? data)
        {
            return new CustomResponse<T>
            {
                Success = true,
                Data = data,
                Error = null
            };
        }

        public static CustomResponse<T> Fail(FailureType failureType, string? message)
        {
            return new CustomResponse<T>
            {
                Success = false,
                Error = ErrorModel.Of(failureType.ToString(), message)
            };
        }

        public static CustomResponse<T> UnUnAuthorizedAccess(string? message)
        {
            return new CustomResponse<T>
            {
                Success = false,
                Error = ErrorModel.Of(FailureType.UnAuthorized.ToString(), message)
            };
        }

        public static CustomResponse<T> BadRequest(string? message)
        {
            return new CustomResponse<T>
            {
                Success = false,
                Error = ErrorModel.Of(FailureType.BadRequest.ToString(), message)
            };
        }

        public static CustomResponse<T> NotFound(string? message)
        {
            return new CustomResponse<T>
            {
                Success = false,
                Error = ErrorModel.Of(FailureType.NotFound.ToString(), message)
            };
        }
    }

    public class CustomResponse
    {
        public bool Success { get; set; }
        public ErrorModel Error { get; set; }

        public static CustomResponse Fail(FailureType failureType, string? message)
        {
            return new CustomResponse
            {
                Success = false,
                Error = ErrorModel.Of(failureType.ToString(), message)
            };
        }
        public static CustomResponse Fail(string? message)
        {
            return new CustomResponse
            {
                Success = false,
                Error = ErrorModel.Of(FailureType.BadRequest.ToString(), message)
            };
        }
    }
    }
}
