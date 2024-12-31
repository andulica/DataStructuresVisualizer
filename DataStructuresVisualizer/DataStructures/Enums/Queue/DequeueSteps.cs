using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresVisualizer.DataStructures.Enums.Queue
{
    /// <summary>
    /// Enum representing the steps involved in a Dequeue operation on a Queue.
    /// </summary>
    public enum DequeueSteps
    {
        /// <summary>
        /// for (i = 0; i < K; ++i)
        /// </summary>
        LoopKTimes = 0,

        /// <summary>
        /// tmp = head
        /// </summary>
        SaveHeadInTmp = 1,

        /// <summary>
        /// head = head.next
        /// </summary>
        MoveHeadToNext = 2,

        /// <summary>
        /// delete tmp
        /// </summary>
        DeleteTmp = 3
    }
}
