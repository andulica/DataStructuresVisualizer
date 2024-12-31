using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresVisualizer.DataStructures.Enums.Queue
{
    /// <summary>
    /// Enum representing the steps involved in an Enqueue operation on a Queue.
    /// </summary>
    public enum EnqueueSteps
    {
        /// <summary>
        /// Vertex vtx = new Vertex(v)
        /// </summary>
        CreateVertex = 0,

        /// <summary>
        /// tail.next = vtx
        /// </summary>
        UpdateTailNextPointer = 1,

        /// <summary>
        /// tail = vtx
        /// </summary>
        UpdateTail = 2
    }
}
