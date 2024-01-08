namespace DataStructuresVisualizer.DataStructures.Queue;

public class QueueStructure<T>
{
    // Internal linked list to store the elements of the queue.
    private LinkedList<T> list = new LinkedList<T>();

    /// <summary>
    /// Adds an item to the end of the queue.
    /// </summary>
    /// <param name="value">The item to add to the queue.</param>
    public void Enqueue(T value)
    {
        list.AddLast(value);
    }

    /// <summary>
    /// Removes and returns the item at the front of the queue.
    /// </summary>
    /// <returns>The item at the front of the queue.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
    public void Dequeue()
    {
        if (list.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        T value = list.First.Value;
        list.RemoveFirst();
    }

    /// <summary>
    /// Returns the item at the front of the queue without removing it.
    /// </summary>
    /// <returns>The item at the front of the queue.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
    public T Peek()
    {
        if (list.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        return list.First.Value;
    }

    /// <summary>
    /// Checks if the queue is empty.
    /// </summary>
    /// <returns>True if the queue is empty; otherwise, false.</returns>
    public bool IsEmpty()
    {
        return list.Count == 0;
    }

    /// <summary>
    /// Returns the number of items in the queue.
    /// </summary>
    /// <returns>The number of items in the queue.</returns>
    public int Size()
    {
        return list.Count;
    }

    /// <summary>
    /// Clears all items from the queue.
    /// </summary>
    public void Clear()
    {
        list.Clear();
    }
}