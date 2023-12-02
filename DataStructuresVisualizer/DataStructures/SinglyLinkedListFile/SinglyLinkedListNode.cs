namespace DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;

public class SinglyLinkedListNode<T>
{
    // The data contained in the node.
    public T _data { get; set; }

    /// <summary>
    /// Gets or sets the next node in the singly linked list.
    /// </summary>
    public SinglyLinkedListNode<T> Next { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SinglyLinkedListNode{T}"/> class with the specified data.
    /// </summary>
    /// <param name="data">The data to store in the node.</param>
    public SinglyLinkedListNode(T data)
    {
        _data = data; // Set the data of the node.
        Next = null;  // Initially, the next node is null.
    }
}