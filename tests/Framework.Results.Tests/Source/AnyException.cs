using System;

namespace Framework.Results.Tests.Source
{
    public class AnyException : Exception
    {
        public AnyException() : base("This is an Exception") { }
    }
}
