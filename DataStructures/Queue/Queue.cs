using System;
using System.Collections.Generic;

public class Queue<T>
{
    private LinkedList<T> list = new LinkedList<T>();

    // Adds an item to the end of the queue
    public void Enqueue(T value)
    {
        list.AddLast(value);
    }

    // Removes and returns the item at the front of the queue
    public T Dequeue()
    {
        if (list.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        T value = list.First.Value;
        list.RemoveFirst();
        return value;
    }

    // Returns the item at the front of the queue without removing it
    public T Peek()
    {
        if (list.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        return list.First.Value;
    }

    // Checks if the queue is empty
    public bool IsEmpty()
    {
        return list.Count == 0;
    }

    // Returns the number of items in the queue
    public int Size()
    {
        return list.Count;
    }

    // Clears the queue
    public void Clear()
    {
        list.Clear();
    }
}