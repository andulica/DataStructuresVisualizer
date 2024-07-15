namespace DataStructuresVisualizer.DataStructures.DoublyLinkedListFile
{
    /// <summary>
    /// Represents a node in a doubly linked list.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the node.</typeparam>
    public class DoublyLinkedListNode<T>
    {
        /// <summary>
        /// Gets or sets the data contained in the node.
        /// </summary>
        public T _data { get; set; }

        // Unique identifier
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the next node in the doubly linked list.
        /// </summary>
        public DoublyLinkedListNode<T> Next { get; set; }

        /// <summary>
        /// Gets or sets the previous node in the doubly linked list.
        /// </summary>
        public DoublyLinkedListNode<T> Prev { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoublyLinkedListNode{T}"/> class.
        /// </summary>
        /// <param name="data">The data to store in the node.</param>
        public DoublyLinkedListNode(T data)
        {
            _data = data; // Set the data of the node.
            Next = null; // Initially, the next node is null.
            Prev = null; // Initially, the previous node is null.
            Id = Guid.NewGuid(); // Generate a unique identifier
        }
    }
}