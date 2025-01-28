using System;

namespace Weld.Binding.Exceptions
{
    /// <summary>
    /// Exception thrown when the requested component on a GameObject could not be found.
    /// </summary>
    public class ComponentNotFoundException : Exception
    {
        public ComponentNotFoundException(string message)
            : base(message)
        {
        }
    }
}
