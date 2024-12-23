
namespace DataStructuresVisualizer.DataStructures.Enums.SinglyLinkedList
{
    /// <summary>
    /// Enum representing the steps involved in the Remlove operation for a SinglyLinkedList.
    /// </summary>
    public enum RemoveSteps
    {
        /// <summary>
        /// if empty, do nothing
        /// </summary>
        CheckIfEmpty = 0,

        /// <summary>
        /// Vertex pre = _head
        /// </summary>
        InitializePreHead = 1,

        /// <summary>
        /// for (k = 0; k<i-1; k++)
        /// </summary>
        LoopToPosition = 2,

        /// <summary>
        /// pre = pre.next
        /// </summary>
        MovePreToNext = 3,

        /// <summary>
        /// Vertex del = pre.next, after = del.next
        /// </summary>
        SetDelAndAfter = 4,

        /// <summary>
        /// pre.next = after
        /// </summary>
        UpdatePreNextToAfter = 5,

        /// <summary>
        /// delete del
        /// </summary>
        DeleteDel = 6
    }
}
