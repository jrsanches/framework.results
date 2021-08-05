using Framework.Results.Exceptions;
using System;

namespace Framework.Results.Models
{
    public class Result : ResultBase
    {
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

        public Result OnFailedThrowsException()
        {
            if (Failed)
                throw new ResultException(this);

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
    }
}
