using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresVisualizer.DataStructures.Enums.Queue
{
    /// <summary>
    /// Enum representing the steps involved in a PeekBack operation on a Queue.
    /// </summary>
    public enum PeekBackSteps
    {
        /// <summary>
        /// if empty, return NOT_FOUND
        /// </summary>
        CheckEmptyReturnNotFound = 0,

        /// <summary>
        /// return tail.item
        /// </summary>
        ReturnTailItem = 1
    }
}
