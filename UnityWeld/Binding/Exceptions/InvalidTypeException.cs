using System;

namespace Weld.Binding.Exceptions
{
    /// <summary>
    /// The supplied type is invalid in the current context.
    /// </summary>
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(string message)
            : base(message)
        {
        }
    }
}
