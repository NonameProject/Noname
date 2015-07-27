using System;

namespace Abitcareer.Core.CustomExceptions
{
    public class JavaScriptException : Exception
    {
        public JavaScriptException(string message)
            : base(message)
        {
        }
    }
}