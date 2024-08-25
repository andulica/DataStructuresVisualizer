
namespace DataStructuresVisualizer.DataStructures.Enums.SinglyLinkedList
{
    /// <summary>
    /// Enum representing the steps involved in the InsertAtPosition operation for a SinglyLinkedList.
    /// </summary>
    public enum InsertAtPositionSteps
    {
        /// <summary>
        /// Vertex pre = head
        /// </summary>
        InitializePreHead = 0,

        /// <summary>
        /// for (k = 0; k<i-1; k++)
        /// </summary>
        LoopToPosition = 1,

        /// <summary>
        /// pre = pre.next
        /// </summary>
        MovePreToNext = 2,

        /// <summary>
        /// Vertex aft = pre.next
        /// </summary>
        SetAftToPreNext = 3,

        /// <summary>
        /// Vertex vtx = new Vertex(v)
        /// </summary>
        CreateVertex = 4,

        /// <summary>
        /// vtx.next = aft
        /// </summary>
        SetVertexNextToAft = 5,

        /// <summary>
        /// pre.next = vtx
        /// </summary>
        SetPreNextToVertex = 6
    }
}
