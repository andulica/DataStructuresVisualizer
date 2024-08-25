using DataStructuresVisualizer.DataStructures;
using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
using System.Collections;
using DataStructuresVisualizer.DataStructures.Enums.SinglyLinkedList;

public class SinglyLinkedList<T> : IEnumerable<SinglyLinkedListNode<T>>
{
    private SinglyLinkedListNode<T>? _head;
    private SinglyLinkedListNode<T>? _tail;
    public Action<int> HighlightRequested { get; set; }

    public Action<PrependSteps, VisualizationTiming> HighlightRequested2 { get; set; }

    public SinglyLinkedListNode<T> Head => _head;
    public SinglyLinkedListNode<T> Tail => _tail;

    // Count of nodes in the singly linked list.
    private int count;

    // Maximum capacity of the singly linked list.
    public readonly int maxCapacity = 6;

    // Public property to access the count.
    public int Count => count;

    protected virtual void OnHighlightRequested(int lineNumber)
    {
        HighlightRequested?.Invoke(lineNumber);
    }

    /// <summary>
    /// Appends a new node containing the specified data to the end of the singly linked list and returns the newly created node.
    /// </summary>
    /// <param name="data">The data to be stored in the new node.</param>
    /// <returns>
    /// The newly created <see cref="SinglyLinkedListNode{T}"/> that was added to the list.
    /// </returns>
    /// <remarks>
    /// This method sets the new node as the last node of the list by updating the tail. If the list is empty,
    /// the new node will serve as both the head and tail. An exception is thrown if the list has already reached its maximum capacity.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the list has reached its maximum capacity and cannot accommodate additional nodes.
    /// </exception>
    public SinglyLinkedListNode<T> Add(T data)
    {
        if (count >= maxCapacity)
        {
            throw new InvalidOperationException("List is full.");
        }

        SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);

        if (_head == null)
        {
            _head = _tail = newNode; // If the list is empty, the new node becomes both the head and the tail.
        }
        else
        {
            _tail.Next = newNode; // Link the new node to the current tail.
            _tail = newNode;      // Update the tail reference to the new node.
        }

        count++; // Increment the node count in the list.
        return newNode;
    }

    /// <summary>
    /// Appends a new node to the end of the singly linked list, with an optional delay to facilitate UI visualization.
    /// </summary>
    /// <param name="newNode">The new node to append to the list.</param>
    /// <param name="delay">The delay in milliseconds before the node is visually appended, defaulting to 0 for no delay.</param>
    /// <returns>The newly appended node.</returns>
    /// <remarks>
    /// This method triggers UI updates through highlighted code lines to visually represent each step of the append operation.
    /// </remarks>
    /// <exception cref="InvalidOperationException">Thrown if the list has reached its maximum capacity.</exception>
    public async Task<SinglyLinkedListNode<T>> AppendAsync(SinglyLinkedListNode<T> newNode, int delay = 0)
    {
        if (Count >= maxCapacity)
        {
            throw new InvalidOperationException("List is full.");
        }

        // Invoke highlighting and delay for visual effect
        HighlightRequested?.Invoke(0); // "Vertex vtx = new Vertex(v)"
        await Task.Delay(delay);

        if (_head == null)
        {
            _head = _tail = newNode; // Set both head and tail to the new node if list was empty
            HighlightRequested?.Invoke(1); // "tail.next = vtx"
            await Task.Delay(delay);
        }
        else
        {
            HighlightRequested?.Invoke(1); // "tail.next = vtx"
            await Task.Delay(delay);
            _tail.Next = newNode;
         
            _tail = newNode;
            HighlightRequested?.Invoke(2); // "tail = vtx"
            await Task.Delay(delay);
        }

        count++;

        return newNode; // Return the newly appended node
    }

    //public class UiUpdate
    //{
    //    public int lineNumber;
    //    public bool nodeadd;
    //    public int delayMS;

    //}

    /// <summary>
    /// Prepends a new node with the specified data to the beginning of the list.
    /// </summary>
    /// <param name="data">The data to prepend to the list.</param>
    public async Task<SinglyLinkedListNode<T>> Prepend(SinglyLinkedListNode<T> newNode, VisualizationTiming timing)
    {
        if (count >= maxCapacity)
        {
            throw new InvalidOperationException("List is full.");
        }
        await Task.Delay(timing.HighlightDelay);


        HighlightRequested2?.Invoke(PrependSteps.CreateVertex, timing);
        await Task.Delay(timing.HighlightDelay);

        HighlightRequested2?.Invoke(PrependSteps.SetNextPointer, timing);
        await Task.Delay(timing.HighlightDelay);
        newNode.Next = _head;

        HighlightRequested2?.Invoke(PrependSteps.SetHead, timing);
        await Task.Delay(timing.HighlightDelay);
        _head = newNode;

        count++;
        return newNode;
    }

    /// <summary>
    /// Inserts a new node with the specified data at the specified index.
    /// If the index is beyond the last position, the node is added at the end.
    /// </summary>
    /// <param name="index">The index at which to insert the node.</param>
    /// <param name="newNode">The new node to insert into the list.</param>
    /// <param name="delay">The delay in milliseconds for visualizing the steps.</param>
    /// <returns>The newly inserted node.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the list has reached its maximum capacity.
    /// </exception>
    public async Task<SinglyLinkedListNode<T>> InsertAt(int index, SinglyLinkedListNode<T> newNode, int delay = 0)
    {
        if (count >= maxCapacity)
        {
            throw new InvalidOperationException("List is full.");
        }

        if (_head == null || index == 0)
        {
            newNode.Next = _head;
            _head = newNode;
            count++;
            HighlightRequested?.Invoke(0); // "head = vtx"
            await Task.Delay(delay);
            return newNode;
        }

        SinglyLinkedListNode<T> current = _head;
        int currentIndex = 0;

        HighlightRequested?.Invoke(0); // "Vertex pre = head"
        await Task.Delay(delay / 6);

        // Traverse until the end of the list or the specified index
        while (current.Next != null && currentIndex < index)
        {
            HighlightRequested?.Invoke(1); // "for (k = 0; k<i-1; k++)"
            await Task.Delay(delay);
            current = current.Next;
            currentIndex++;
            HighlightRequested?.Invoke(2); // "pre = pre.next"
            await Task.Delay(delay);

            if (currentIndex == index)
            {
                HighlightRequested?.Invoke(3); // "Vertex aft = pre.next"
                await Task.Delay(delay / 4);
            }
        }
        HighlightRequested?.Invoke(4); // "Vertex vtx = new Vertex(v)"
        await Task.Delay(delay);

        HighlightRequested?.Invoke(5); // "vtx.next = aft"
        await Task.Delay(delay + 1000);

        // Insert the new node
        newNode.Next = current.Next;
        current.Next = newNode;
        count++;

        HighlightRequested?.Invoke(6); // "pre.next = vtx"
        await Task.Delay(delay);

        return newNode;
    }

    ///// <summary>
    ///// Deletes the first occurrence of a node with the specified data from the list asynchronously.
    ///// </summary>
    ///// <param name="data">The data of the node to delete.</param>
    //public async Task Delete(T data)
    //{
    //    if (_head == null)
    //    {
    //        HighlightRequested?.Invoke(0); // "if empty, do nothing"
    //        await Task.Delay(500);
    //        return; // Check if the list is empty and exit
    //    }

    //    HighlightRequested?.Invoke(1); // "Vertex pre = head"
    //    await Task.Delay(500);

    //    SinglyLinkedListNode<T> current = _head;
    //    if (EqualityComparer<T>.Default.Equals(current._data, data))
    //    {
    //        _head = _head.Next; // Move head to next node, effectively deleting it
    //        count--;
    //        HighlightRequested?.Invoke(6); // "delete del"
    //        await Task.Delay(500);
    //        return;
    //    }

    //    while (current.Next != null)
    //    {
    //        HighlightRequested?.Invoke(2); // "for (k = 0; k<i-1; k++)"
    //        await Task.Delay(500);

    //        if (EqualityComparer<T>.Default.Equals(current.Next._data, data))
    //        {
    //            current.Next = current.Next.Next; // Disconnect the node from the list
    //            count--;
    //            HighlightRequested?.Invoke(5); // "pre.next = after"
    //            await Task.Delay(500);
    //            return;
    //        }

    //        current = current.Next;
    //        HighlightRequested?.Invoke(3); // "pre = pre.next"
    //        await Task.Delay(500);
    //    }

    //    HighlightRequested?.Invoke(4); // "Vertex del = pre.next, after = del.next"
    //    await Task.Delay(500);
    //}

    /// <summary>
    /// Deletes a node at the specified index from the singly linked list asynchronously.
    /// </summary>
    /// <remarks>
    /// This method allows for deletion of nodes at any position within the list.
    /// It can handle deletion of the head or tail nodes, as well as any node in between.
    /// The list is zero-indexed.
    /// </remarks>
    /// <param name="index">The zero-based index of the node to be deleted.</param>
    /// <param name="delay">The delay in milliseconds for visualizing the steps.</param>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when the specified index is less than 0 or greater than or equal to the size of the list.
    /// </exception>
    public async Task DeleteAt(int index, int delay)
    {
        if (index < 0 || index >= count)
        {
            throw new IndexOutOfRangeException($"Index {index} is out of range for the linked list.");
        }

        if (_head == null)
        {
            HighlightRequested?.Invoke(0); // "if empty, do nothing"
            await Task.Delay(delay);
            return; // Check if the list is empty and exit
        }

        SinglyLinkedListNode<T> current = _head;

        if (index == 0)
        {
            HighlightRequested?.Invoke(1); // "Vertex pre = head"
            await Task.Delay(delay);

            _head = _head.Next; // Move head to next node, effectively deleting it
            count--;
            HighlightRequested?.Invoke(6); // "delete del"
            await Task.Delay(delay);
            return;
        }

        HighlightRequested?.Invoke(1); // "Vertex pre = head"
        await Task.Delay(delay / 4);

        for (int i = 0; i < index - 1; i++)
        {
            HighlightRequested?.Invoke(2); // "for (k = 0; k<i-1; k++)"
            await Task.Delay(delay);

            current = current.Next;

            HighlightRequested?.Invoke(3); // "pre = pre.next"
            await Task.Delay(delay);
        }

        HighlightRequested?.Invoke(4); // "Vertex del = pre.next, after = del.next"
        await Task.Delay(delay);

        current.Next = current.Next?.Next;
        count--;

        HighlightRequested?.Invoke(5); // "pre.next = after"
        await Task.Delay(delay);

        HighlightRequested?.Invoke(6); // "delete del"
        await Task.Delay(delay);
    }

    /// <summary>
    /// Searches for a node with the specified data and returns it.
    /// </summary>
    /// <param name="data">The data to search for in the list.</param>
    /// <returns>The node containing the data if found in the list; otherwise, null.</returns>
    public async Task<SinglyLinkedListNode<T>> Search(T data, int delay)
    {
        // Check if the list is empty and handle the first line of the script
        if (_head == null)
        {
            OnHighlightRequested(0); // "if empty, return NOT_FOUND"
            await Task.Delay(delay);
            return null;
        }
        OnHighlightRequested(1); // "index = 0, tmp = head"
        await Task.Delay(delay / 4);

        SinglyLinkedListNode<T> current = _head;
        int position = 0;

        while (current != null)
        {
            OnHighlightRequested(2); // "while (tmp.item != v)"
            await Task.Delay(delay);

            if (EqualityComparer<T>.Default.Equals(current._data, data))
            {
                OnHighlightRequested(6); // "return index"
                await Task.Delay(delay);
                return current;
            }

            // Update the tmp and index for the next loop iteration
            current = current.Next;
            position++;

            // Check if the next node is null to handle the last if condition
            if (current == null)
            {
                OnHighlightRequested(4); // "if tmp == null"
                await Task.Delay(delay);
                OnHighlightRequested(5); // "return NOT_FOUND"
                await Task.Delay(delay);
            }
            else
            {
                OnHighlightRequested(3); // "index++, tmp = tmp.next"
                await Task.Delay(delay);
            }
        }

        return null;
    }

    /// <summary>
    /// Removes and returns the head node of the singly linked list.
    /// </summary>
    /// <returns>The node that was removed from the front of the list.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the list is empty.</exception>
    public SinglyLinkedListNode<T> FindHead()
    {
        if (_head == null)
        {
            throw new InvalidOperationException("List is empty.");
        }

        SinglyLinkedListNode<T> deletedNode = _head;
        _head = _head.Next;
        count--;

        return deletedNode;
    }

    /// <summary>
    /// Finds the node at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the node to find.</param>
    /// <returns>The node at the specified index, or null if the index is out of range.</returns>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when the specified index is less than 0 or greater than or equal to the size of the list.
    /// </exception>
    public SinglyLinkedListNode<T> FindNodeByIndex(int index)
    {
        if (index < 0 || index >= count)
        {
            throw new IndexOutOfRangeException($"Index {index} is out of range for the linked list.");
        }

        SinglyLinkedListNode<T> current = _head;
        int currentIndex = 0;

        while (current != null)
        {
            if (currentIndex == index)
            {
                return current;
            }
            current = current.Next;
            currentIndex++;
        }

        return null; // Should not reach here if the index is valid
    }

    /// <summary>
    /// Determines whether the singly linked list has reached its maximum capacity.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the count of nodes in the list is greater than or equal to the maximum capacity; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method is used to check if any further insertions are allowed in the singly linked list.
    /// </remarks>
    public bool IsFull()
    {
        return count >= maxCapacity;
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