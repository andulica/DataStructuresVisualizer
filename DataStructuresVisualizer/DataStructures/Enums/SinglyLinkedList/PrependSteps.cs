
namespace DataStructuresVisualizer.DataStructures.Enums.SinglyLinkedList
{
    /// <summary>
    /// Enum representing the steps involved in the Prepend operation for a SinglyLinkedList.
    /// </summary>
    public enum PrependSteps
    {
        /// <summary>
        /// Step where a new vertex is created with the given value.
        /// </summary>
        CreateVertex = 0,

        /// <summary>
        /// Step where the new vertex's next pointer is set to the current head of the list.
        /// </summary>
        SetNextPointer = 1,

        /// <summary>
        /// Step where the head of the list is updated to point to the new vertex.
        /// </summary>
        SetHead = 2
    }
}
