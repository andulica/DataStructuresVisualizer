
namespace DataStructuresVisualizer.DataStructures.Enums.SinglyLinkedList
{
    /// <summary>
    /// Enum representing the steps involved in the Search operation for a SinglyLinkedList.
    /// </summary>
    public enum SearchSteps
    {
        /// <summary>
        /// Step where the list is checked to see if it is empty; if it is, return NOT_FOUND.
        /// </summary>
        CheckEmptyReturnNotFound = 0,

        /// <summary>
        /// Step where the search index is initialized and the _head of the list is set as the starting point.
        /// </summary>
        InitializeIndexAndHead = 1,

        /// <summary>
        /// Step where the loop starts to iterate through the list until the value is found.
        /// </summary>
        LoopUntilFound = 2,

        /// <summary>
        /// Step where the index is incremented and the current node is moved to the next node in the list.
        /// </summary>
        IncrementIndexAndMoveNext = 3,


        /// <summary>
        /// Step where the values is not found and null is returned.
        /// </summary>
        ReturnNull = 4,

        /// <summary>
        /// Step where the index of the found value is returned.
        /// </summary>
        ReturnIndex = 5
    }
}
