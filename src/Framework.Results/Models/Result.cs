using Framework.Results.Exceptions;
using System;

namespace Framework.Results.Models
{
    public class Result
    {
        public bool Succeeded { get; protected set; }
        public bool Failed => !Succeeded;
        public string Message { get; protected set; }
        public Exception Exception { get; protected set; }

        public static Result Success()
        {
            return new Result()
            {
                Succeeded = true
            };
        }

        public static Result Success(string message)
        {
            return new Result()
            {
                Succeeded = true,
                Message = message
            };
        }

        public static Result Fail(string message)
        {
            return new Result()
            {
                Succeeded = false,
                Message = message
            };
        }

        public static Result Fail(Exception exception)
        {
            return new Result()
            {
                Succeeded = false,
                Message = exception.Message,
                Exception = exception
            };
        }

        public static Result Fail(Exception exception, string message)
        {
            return new Result()
            {
                Succeeded = false,
                Message = message,
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
