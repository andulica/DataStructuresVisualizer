﻿using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
using System.Collections;

public class SinglyLinkedList<T> : IEnumerable<SinglyLinkedListNode<T>>
{
    // The head node of the singly linked list.
    public SinglyLinkedListNode<T> head { get; set; }

    // Count of nodes in the singly linked list.
    private int count;

    // Maximum capacity of the singly linked list.
    private int maxCapacity;

    // Public property to access the count.
    public int Count => count;

    /// <summary>
    /// Initializes a new instance of the <see cref="SinglyLinkedList{T}"/> class with a specified maximum capacity.
    /// </summary>
    /// <param name="capacity">
    /// The maximum number of nodes that can be added to the linked list. 
    /// The default value is 6.
    /// </param>
    public SinglyLinkedList(int capacity = 6)
    {
        maxCapacity = capacity;
        count = 0;
        head = null;
    }

    /// <summary>
    /// Appends a new node with the specified data to the end of the list.
    /// </summary>
    /// <param name="data">The data to append to the list.</param>
    public void Append(T data)
    {
        if (count >= maxCapacity)
        {
            return;
        }

            SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            SinglyLinkedListNode<T> current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
        count++;
    }

    /// <summary>
    /// Prepends a new node with the specified data to the beginning of the list.
    /// </summary>
    /// <param name="data">The data to prepend to the list.</param>
    public void Prepend(T data)
    {
        if (count >= maxCapacity)
        {
            return;
        }

        SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);

        newNode.Next = head;
        head = newNode;
        count++;
    }

    /// <summary>
    /// Inserts a new node with the specified data at the specified index.
    /// If the index is beyond the last position, the node is added at the end.
    /// </summary>
    /// <param name="index">The index at which to insert the node.</param>
    /// <param name="data">The data to insert into the list.</param>
    /// <returns>The newly inserted node.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the list has reached its maximum capacity.
    /// </exception>
    public SinglyLinkedListNode<T> InsertAt(int index, T data)
    {
        if (count >= maxCapacity)
        {
            return null;
        }

        SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);

        if (head == null || index == 0)
        {
            newNode.Next = head;
            head = newNode;
            count++;
            return newNode;
        }

        SinglyLinkedListNode<T> current = head;
        int currentIndex = 0;

        // Traverse until the end of the list or the specified index
        while (current.Next != null && currentIndex < index - 1)
        {
            current = current.Next;
            currentIndex++;
        }

        // Insert the new node
        newNode.Next = current.Next;
        current.Next = newNode;
        count++;
        return newNode;
    }

    /// <summary>
    /// Deletes the first occurrence of a node with the specified data from the list.
    /// </summary>
    /// <param name="data">The data of the node to delete.</param>
    public void Delete(T data)
    {
        if (head == null) return; // Check if the list is empty

        // Check if the head contains the data to be deleted
        if (EqualityComparer<T>.Default.Equals(head._data, data))
        {
            head = head.Next; // Delete the head node
            count--;
            return;
        }

        SinglyLinkedListNode<T> current = head;
        while (current.Next != null && !EqualityComparer<T>.Default.Equals(current.Next._data, data))
        {
            current = current.Next; // Traverse the list to find the node before the target node
        }

        // If the node after the current node needs to be deleted
        if (current.Next != null)
        {
            current.Next = current.Next.Next; // Delete the node
            count--;
        }
    }

    /// <summary>
    /// Deletes a node at the specified index from the singly linked list.
    /// </summary>
    /// <remarks>
    /// This method allows for deletion of nodes at any position within the list.
    /// It can handle deletion of the head or tail nodes, as well as any node in between.
    /// The list is zero-indexed.
    /// </remarks>
    /// <param name="index">The zero-based index of the node to be deleted.</param>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when the specified index is less than 0 or greater than or equal to the size of the list.
    /// </exception>
    public void DeleteAt(int index)
    {
        if (index < 0 || index >= count)
        {
            throw new IndexOutOfRangeException($"Index {index} is out of range for the linked list.");
        }

        if (index == 0)
        {
            DeleteHead();
            return;
        }

        if (index == count - 1)
        {
            DeleteTail();
            return;
        }

        SinglyLinkedListNode<T> current = head;
        for (int i = 0; i < index - 1; i++)
        {
            current = current.Next;
        }

        current.Next = current.Next?.Next;
        count--;
    }

    /// <summary>
    /// Deletes the head node of the list and returns its data. Used for stack-like operations.
    /// </summary>
    /// <returns>The data of the deleted head node.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the list is empty.</exception>
    public T DeleteHeadForStack()
    {
        if (head == null)
        {
            throw new InvalidOperationException("The list is empty.");
        }

        T value = head._data;
        head = head.Next;
        count--;
        return value;
    }

    /// <summary>
    /// Deletes the head node of the list.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the list is empty.</exception>
    public void DeleteHead()
    {
        if (head == null)
        {
            throw new InvalidOperationException("List is empty.");
        }
        head = head.Next;
        count--;
    }

    /// <summary>
    /// Deletes the last node of the list.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the list is empty.</exception>
    public void DeleteTail()
    {
        if (head == null)
        {
            throw new InvalidOperationException("List is empty.");
        }

        if (head.Next == null)
        {
            head = null;
            count--;
            return;
        }

        SinglyLinkedListNode<T> current = head;
        while (current.Next.Next != null)
        {
            current = current.Next;
        }
        current.Next = null;
        count--;
    }

    /// <summary>
    /// Searches for a node with the specified data and returns it.
    /// </summary>
    /// <param name="data">The data to search for in the list.</param>
    /// <returns>The node containing the data if found in the list; otherwise, null.</returns>
    public SinglyLinkedListNode<T> Search(T data)
    {
        SinglyLinkedListNode<T> current = head;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current._data, data))
            {
                return current;  // Return the node itself, not just the data
            }
            current = current.Next;
        }
        return null;
    }

    /// <summary>
    /// Searches for the first occurrence of the specified value within the linked list and returns its index.
    /// </summary>
    /// <param="data">The value to search for in the list.</param>
    /// <returns>The zero-based index of the first occurrence of the value in the list, if found.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the value is not present in the list.</exception>
    public int FindIndexOf(T data)
    {
        SinglyLinkedListNode<T> current = head;
        int index = 0;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current._data, data))
            {
                return index;
            }
            current = current.Next;
            index++;
        }
        return -1;
    }

    /// <summary>
    /// Returns a string representation of the linked list.
    /// </summary>
    /// <returns>A string representing the linked list.</returns>
    public override string ToString()
    {
        string result = "";
        SinglyLinkedListNode<T> current = head;
        while (current != null)
        {
            result += current._data + " -> ";
            current = current.Next;
        }
        return result + "null";
    }

    /// <summary>
    /// Determines whether the list contains a specific value.
    /// </summary>
    /// <param name="value">The value to locate in the list.</param>
    /// <returns>True if the value is found; otherwise, false.</returns>
    public bool Contains(T value)
    {
        SinglyLinkedListNode<T> current = head;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current._data, value))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the linked list.
    /// </summary>
    /// <returns>An IEnumerator for the linked list.</returns>
    public IEnumerator<SinglyLinkedListNode<T>> GetEnumerator()
    {
        SinglyLinkedListNode<T> current = head;
        while (current != null)
        {
            yield return current;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}