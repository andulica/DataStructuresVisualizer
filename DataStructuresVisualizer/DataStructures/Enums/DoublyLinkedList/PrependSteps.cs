
namespace DataStructuresVisualizer.DataStructures.Enums.DoublyLinkedList
{
    /// <summary>
    /// Enum representing the steps involved in the PrependAsync operation for a SinglyLinkedList.
    /// </summary>
    public enum PrependSteps
    {
        /// <summary>
        /// Step where a new vertex is created with the given value.
        /// </summary>
        CreateVertex = 0,

        SetPreviousPointer = 1,

        /// <summary>
        /// Step where the new vertex's next pointer is set to the current _head of the list.
        /// </summary>
        SetNextPointer = 2,

        /// <summary>
        /// Step where the _head of the list is updated to point to the new vertex.
        /// </summary>
        SetHead = 3
    }
}
