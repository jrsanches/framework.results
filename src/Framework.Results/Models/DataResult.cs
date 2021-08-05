using Framework.Results.Exceptions;
using System;

namespace Framework.Results.Models
{
    public class Result<T> : ResultBase
    {
        public T Data { get; private set; }

        public static Result<T> Success()
        {
            return new Result<T>()
            {
                Succeeded = true,
                Data = default
            };
        }

        public static Result<T> Success(T data, string message = default)
        {
            return new Result<T>()
            {
                Succeeded = true,
                Message = message,
                Data = data
            };
        }

        public static Result<T> Fail(T data = default)
        {
            return new Result<T>()
            {
                Succeeded = false,
                Data = data
            };
        }

        public static Result<T> Fail(string message, T data = default)
        {
            return new Result<T>()
            {
                Succeeded = false,
                Message = message,
                Data = data
            };
        }

        public static Result<T> Fail(Exception exception, T data = default)
        {
            return new Result<T>()
            {
                Succeeded = false,
                Message = exception.Message,
                Data = data,
                Exception = exception
            };
        }

        public static Result<T> Fail(Exception exception, string message, T data = default)
        {
            return new Result<T>()
            {
                Succeeded = false,
                Message = message,
                Data = data,
                Exception = exception
            };
        }

        public T OnFailedThrowsDataException()
        {
            if (Failed)
                throw new ResultDataException<T>(this);

            return Data;
        }

        public T OnFailedThrowsException()
        {
            if (Failed)
                throw new ResultException(new Result(Succeeded, Exception, Message));

            return Data;
        }

        public Result<T> OnFailed(Action<Result<T>> action)
        {
            if (Failed)
                action.Invoke(this);

            return this;
        }

        public Result<T> OnSuccess(Action<Result<T>> action)
        {
            if (Succeeded)
                action.Invoke(this);

            return this;
        }
    }
}
