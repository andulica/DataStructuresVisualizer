namespace DataStructuresVisualizer.DataStructures.Queue;

using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
using System;
using System.Collections;
using System.Collections.Generic;

public class QueueStructure<T> : IEnumerable<T>
{
    /// <summary>
    /// The internal singly linked list to store the elements of the queue.
    /// </summary>
    private SinglyLinkedList<T> list = new SinglyLinkedList<T>();

    /// <summary>
    /// Adds an item to the end of the queue.
    /// </summary>
    /// <param name="value">The item to add to the queue.</param>
    public void Enqueue(T value)
    {
        list.Append(value);
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

        if (list.Count < count)
        {
            throw new InvalidOperationException("The queue does not have enough items.");
        }

        for (int i = 0; i < count; i++)
        {
            list.DeleteHead();
        }
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

        return list.head._data;
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
    /// Returns an enumerator that iterates through the queue.
    /// </summary>
    /// <returns>An IEnumerator of type T.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        SinglyLinkedListNode<T> current = list.head;
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

