namespace ECommerce.Services.Models.Outputs.Base
{
    public class ErrorModel
    {
        public string? Message { get; set; }
        public string FailureType { get; set; }
        private ErrorModel(){}
        public static ErrorModel Of(string failureType, string? message)
        {
            return new ErrorModel
            {
                Message = message ?? "",
                FailureType = failureType,
            };
        }
    }
}
