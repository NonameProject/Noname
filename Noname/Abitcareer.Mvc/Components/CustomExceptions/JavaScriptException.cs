using System;

namespace Abitcareer.Mvc.Components.CustomExceptions
{
    public class JavaScriptException : Exception
    {
        public JavaScriptException(string message)
            : base(message)
        {
        }
    }
}