using System;

namespace Framework.Results.Models
{
    public abstract class ResultBase
    {
        public bool Succeeded { get; protected set; }
        public bool Failed => !Succeeded;
        public string Message { get; protected set; }
        public Exception Exception { get; protected set; }
    }
}
