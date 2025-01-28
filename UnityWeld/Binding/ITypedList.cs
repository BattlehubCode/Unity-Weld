using System;
using System.Collections;

namespace Weld.Binding
{
    /// <summary>
    /// A list that exposes the type of its items as a property.
    /// </summary>
    public interface ITypedList : IList
    {
        /// <summary>
        /// Specifies the type of items in the list.
        /// </summary>
        Type ItemType { get; }
    }

}
