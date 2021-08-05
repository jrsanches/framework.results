using Framework.Results.Models;
using System;

namespace Framework.Results.Exceptions
{
    public class ResultDataException<T> : Exception
    {
        public readonly Result<T> Result;

        public ResultDataException(Result<T> result) : base(result.Message, result.Exception)
        {
            Result = result;
        }
    }
}
