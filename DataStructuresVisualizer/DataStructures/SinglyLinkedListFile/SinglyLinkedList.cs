﻿using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
using System.Collections;

public class SinglyLinkedList<T> : IEnumerable<SinglyLinkedListNode<T>>
{
    private SinglyLinkedListNode<T>? _head;
    private SinglyLinkedListNode<T>? _tail;
    public Action<int> HighlightRequested { get; set; }

    public SinglyLinkedListNode<T> Head => _head;
    public SinglyLinkedListNode<T> Tail => _tail;

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
        _head = null;
    }

    protected virtual void OnHighlightRequested(int lineNumber)
    {
        HighlightRequested?.Invoke(lineNumber);
    }

    public void Add(T data)
    {
        if (count >= maxCapacity)
        {
            throw new InvalidOperationException("List is full.");
        }

        SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);

        if (_head == null)
        {
            _head = _tail = newNode; // If the list is empty, the new node becomes both head and tail.
        }
        else
        {
            _tail.Next = newNode; // Append the new node to the end of the list.
            _tail = newNode;      // Update the tail to the new node.
        }

        count++; // Increment the count of nodes in the list.
    }

    /// <summary>
    /// Appends a new node with the specified data to the end of the list.
    /// </summary>
    /// <param name="data">The data to append to the list.</param>
    public async Task Append(SinglyLinkedListNode<T> newNode, int delay = 0)
    {
        if (Count >= maxCapacity)
        {
            throw new InvalidOperationException("List is full.");
        }

        // Invoke highlighting and delay for visual effect
        HighlightRequested?.Invoke(0); // "Vertex vtx = new Vertex(v)"
        await Task.Delay(delay);  // Delay before proceeding, default is 0 if not specified

        if (_head == null)
        {
            _head = _tail = newNode; // Set both head and tail to the new node if list was empty
            HighlightRequested?.Invoke(1); // Highlight that this node is now the tail
            await Task.Delay(1000); // Further delay to visualize this step
        }
        else
        {
            _tail.Next = newNode; // Link the new node at the end of the list
            _tail = newNode; // Update the tail to the new node
            HighlightRequested?.Invoke(2); // Highlight that the tail has been updated
            await Task.Delay(1000); // Further delay to visualize this step
        }

        count++; // Increment the count of nodes
    }


    /// <summary>
    /// Prepends a new node with the specified data to the beginning of the list.
    /// </summary>
    /// <param name="data">The data to prepend to the list.</param>
    public void Prepend(T data)
    {
        if (count >= maxCapacity)
        {
            throw new InvalidOperationException("List is full.");
        }

        SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);

        newNode.Next = _head;
        _head = newNode;
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
            throw new InvalidOperationException("List is full.");
        }

        SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);

        if (_head == null || index == 0)
        {
            newNode.Next = _head;
            _head = newNode;
            count++;
            return newNode;
        }

        SinglyLinkedListNode<T> current = _head;
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
    /// <returns>The node that was deleted, or null if no node was found with the specified data.</returns>
    public SinglyLinkedListNode<T> Delete(T data)
    {
        if (_head == null) return null; // Check if the list is empty and return null

        SinglyLinkedListNode<T> nodeToDelete = null;

        // Check if the head contains the data to be deleted
        if (EqualityComparer<T>.Default.Equals(_head._data, data))
        {
            nodeToDelete = _head;
            _head = _head.Next; // Move head to next node, effectively deleting it
            count--;
            return nodeToDelete;
        }

        SinglyLinkedListNode<T> current = _head;
        while (current.Next != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Next._data, data))
            {
                nodeToDelete = current.Next;
                current.Next = current.Next.Next; // Disconnect the node from the list
                count--;
                return nodeToDelete;
            }
            current = current.Next;
        }

        return null; // Return null if no node was found and deleted
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

        SinglyLinkedListNode<T> current = _head;
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
        if (_head == null)
        {
            throw new InvalidOperationException("The list is empty.");
        }

        T value = _head._data;
        _head = _head.Next;
        count--;
        return value;
    }

    /// <summary>
    /// Deletes the head node of the list.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the list is empty.</exception>
    public void DeleteHead()
    {
        if (_head == null)
        {
            throw new InvalidOperationException("List is empty.");
        }
        _head = _head.Next;
        count--;
    }

    /// <summary>
    /// Deletes the last node of the list.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the list is empty.</exception>
    public void DeleteTail()
    {
        if (_head == null)
        {
            throw new InvalidOperationException("List is empty.");
        }

        if (_head.Next == null)
        {
            _head = null;
            count--;
            return;
        }

        SinglyLinkedListNode<T> current = _head;
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
    public async Task<SinglyLinkedListNode<T>> Search(T data)
    {
        // Check if the list is empty and handle the first line of the script
        if (_head == null)
        {
            OnHighlightRequested(0); // "if empty, return NOT_FOUND"
            await Task.Delay(200);
            return null;
        }
        OnHighlightRequested(1); // "index = 0, tmp = head"
        await Task.Delay(200);

        SinglyLinkedListNode<T> current = _head;
        int position = 0;

        while (current != null)
        {
            OnHighlightRequested(2); // "while (tmp.item != v)"
            await Task.Delay(500);

            if (EqualityComparer<T>.Default.Equals(current._data, data))
            {
                OnHighlightRequested(6); // "return index"
                await Task.Delay(500);
                return current;
            }

            // Update the tmp and index for the next loop iteration
            current = current.Next;
            position++;

            // Check if the next node is null to handle the last if condition
            if (current == null)
            {
                OnHighlightRequested(4); // "if tmp == null"
                await Task.Delay(500);
                OnHighlightRequested(5); // "return NOT_FOUND"
                await Task.Delay(500);
            }
            else
            {
                OnHighlightRequested(3); // "index++, tmp = tmp.next"
                await Task.Delay(500);
            }
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
        SinglyLinkedListNode<T> current = _head;
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
        SinglyLinkedListNode<T> current = _head;
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
        SinglyLinkedListNode<T> current = _head;
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
    /// Returns the index of the head node in the linked list.
    /// </summary>
    /// <returns>The index of the head node, or -1 if the list is empty.</returns>
    public int GetHeadIndex()
    {
        if (_head == null)
        {
            return -1;
        }

        int index = 0;
        SinglyLinkedListNode<T> current = _head;
        while (current != null)
        {
            if (current == _head)
            {
                return index;
            }
            current = current.Next;
            index++;
        }
        return -1; // Head not found (should not happen in a valid list)
    }

    /// <summary>
    /// Returns the index of the tail node in the linked list.
    /// </summary>
    /// <returns>The index of the tail node, or -1 if the list is empty.</returns>
    public int GetTailIndex()
    {
        if (_head == null)
        {
            return -1;
        }

        int index = 0;
        SinglyLinkedListNode<T> current = _head;
        while (current.Next != null)
        {
            current = current.Next;
            index++;
        }
        return index;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the linked list.
    /// </summary>
    /// <returns>An IEnumerator for the linked list.</returns>
    public IEnumerator<SinglyLinkedListNode<T>> GetEnumerator()
    {
        SinglyLinkedListNode<T> current = _head;
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