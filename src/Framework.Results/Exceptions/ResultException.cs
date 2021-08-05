using Framework.Results.Models;
using System;

namespace Framework.Results.Exceptions
{
    public class ResultException : Exception
    {
        public readonly Result Result;

        public ResultException(Result result)
            : base(result.Message, result.Exception)
        {
            Result = result;
        }
    }
}
