using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DesafioEclipseworks.WebAPI.Domain.Shared
{
    public class Result<TResult>
    {
        public readonly TResult? Value;

        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;
        public string? ErrorMessage { get; set; } = string.Empty;

        private Result(TResult? value, bool isSuccess)
        {
            Value = value;
            IsSuccess = isSuccess;
        }

        private Result(string errorMessage, bool isSuccess)
        {
            Value = default;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static implicit operator Result<TResult>(TResult? value) => new(value, true);
        public static implicit operator Result<TResult>(string errorMessage) => new(errorMessage, false);
    }

    public class Result { }
}
