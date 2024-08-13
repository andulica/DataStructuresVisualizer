namespace DataStructuresVisualizer.DataStructures.Queue;

using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
using System;
using System.Collections;
using System.Collections.Generic;

public class QueueStructure<T> : SinglyLinkedList<T>, IEnumerable<T>
{
    public QueueStructure() : base() { }

    /// <summary>
    /// Adds an item to the end of the queue.
    /// </summary>
    /// <param name="value">The item to add to the queue.</param>
    public void Enqueue(T value)
    {
        // Add to the end of the linked list
        Add(value);
    }

    /// <summary>
    /// Removes a specified number of items from the front of the queue.
    /// </summary>
    /// <param name="count">The number of items to remove.</param>
    /// <exception cref="InvalidOperationException">Thrown if the queue does not have enough items.</exception>
    public void Dequeue(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than 0.");
        }

        if (Count < count)
        {
            throw new InvalidOperationException("The queue does not have enough items.");
        }

        for (int i = 0; i < count; i++)
        {
            DeleteHead();
        }
    }

    /// <summary>
    /// Returns the item at the front of the queue without removing it.
    /// </summary>
    /// <returns>The item at the front of the queue.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
    public T Peek()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        return Head._data;
    }

    /// <summary>
    /// Checks if the queue is empty.
    /// </summary>
    /// <returns>True if the queue is empty; otherwise, false.</returns>
    public bool IsEmpty()
    {
        return Count == 0;
    }

    /// <summary>
    /// Returns the number of items in the queue.
    /// </summary>
    /// <returns>The number of items in the queue.</returns>
    public int Size()
    {
        return Count;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the queue.
    /// </summary>
    /// <returns>An IEnumerator of type T.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        SinglyLinkedListNode<T> current = Head;
        while (current != null)
        {
            yield return current._data;
            current = current.Next;
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
