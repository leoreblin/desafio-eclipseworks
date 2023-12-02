namespace DesafioEclipseworks.WebAPI.Domain.Shared
{
    public class Result<TResult>
    {
        public readonly TResult? Value;

        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; set; } = Error.None;

        private Result(TResult? value, bool isSuccess)
        {
            Value = value;
            IsSuccess = isSuccess;
        }

        private Result(Error error, bool isSuccess)
        {
            Value = default;
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result<TResult> Success(TResult value) => new(value, true);

        public static Result<TResult> Failure(Error error) => new(error, false);

        public static implicit operator Result<TResult>(TResult? value) => new(value, true);
        public static implicit operator Result<TResult>(Error error) => new(error, false);
    }

    public class Result
    {
        private Result(bool isSuccess, Error error)
        {
            if (IsInvalidError(isSuccess, error))
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);

        private static bool IsInvalidError(bool isSuccess, Error error) => isSuccess && error != Error.None || !isSuccess && error == Error.None;
    }
}
