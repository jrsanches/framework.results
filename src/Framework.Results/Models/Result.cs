using Framework.Results.Exceptions;
using System;
using System.Threading.Tasks;

namespace Framework.Results.Models
{
    public class Result
    {
        public bool Succeeded { get; protected set; }
        public bool Failed => !Succeeded;
        public string Message { get; protected set; }
        public Exception Exception { get; protected set; }

        public Result() { }

        public Result(bool succeeded, Exception exception = default, string message = default)
        {
            Succeeded = succeeded;
            Exception = exception;
            Message = message;
        }

        public static Result Success(string message = default)
        {
            return new Result()
            {
                Succeeded = true,
                Message = message
            };
        }

        public static Result Fail(string message = default)
        {
            return new Result()
            {
                Succeeded = false,
                Message = message
            };
        }

        public static Result Fail(Exception exception, string message = default)
        {
            return new Result()
            {
                Succeeded = false,
                Message = message ?? exception.Message,
                Exception = exception
            };
        }

        public Result GetSuccessOrThrowsException()
        {
            if (Failed)
                throw new ResultException(this);

            return this;
        }

        public Result GetSuccessOrThrows<TE>() where TE : Exception, new()
        {
            if (Failed)
                throw new TE();

            return this;
        }

        public Result OnFailed(Action<Result> action)
        {
            if (Failed)
                action.Invoke(this);

            return this;
        }

        public Result OnSuccess(Action<Result> action)
        {
            if (Succeeded)
                action.Invoke(this);

            return this;
        }

        public async Task<Result> OnFailedReturn(Func<Result, Task<Result>> function)
        {
            if (Failed)
                return await function.Invoke(this);

            return this;
        }

        public async Task<Result> OnSuccessReturn(Func<Result, Task<Result>> function)
        {
            if (Succeeded)
                return await function.Invoke(this);

            return this;
        }
    }
}
