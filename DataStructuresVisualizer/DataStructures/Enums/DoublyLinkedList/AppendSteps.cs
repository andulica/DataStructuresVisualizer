
namespace DataStructuresVisualizer.DataStructures.Enums.DoublyLinkedList;

/// <summary>
/// Enum representing the steps involved in the AppendInstant operation for a SinglyLinkedList.
/// </summary>
public enum AppendSteps
{
    /// <summary>
    /// Step where a new vertex is created with the given value.
    /// </summary>
    CreateVertex = 0,

    /// <summary>
    /// Step where the current _tail's next pointer is updated to point to the new vertex.
    /// </summary>
    UpdateTailNextPointer = 1,

    /// <summary>
    /// Step where the _tail of the list is updated to point to the new vertex.
    /// </summary>
    UpdateTail = 2
}
