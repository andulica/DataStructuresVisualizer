using DataStructuresVisualizer.DataStructures;
using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
using System.Collections;
using DataStructuresVisualizer.DataStructures.Enums.SinglyLinkedList;
using System.Threading;

public class SinglyLinkedList<T> : IEnumerable<SinglyLinkedListNode<T>>
{
    private SinglyLinkedListNode<T>? _head;
    private SinglyLinkedListNode<T>? _tail;

    /// <summary>
    /// A delegate for highlighting steps during asynchronous operations.
    /// </summary>
    public Func<Enum, Task> HighlightRequested;

    /// <summary>
    /// Gets the head node of the singly linked list.
    /// </summary>
    public SinglyLinkedListNode<T>? Head => _head;

    /// <summary>
    /// Gets the tail node of the singly linked list.
    /// </summary>
    public SinglyLinkedListNode<T>? Tail => _tail;

    private int count;

    /// <summary>
    /// Gets the number of nodes in the singly linked list.
    /// </summary>
    public int Count => count;

    /// <summary>
    /// The maximum capacity of the singly linked list.
    /// </summary>
    public readonly int maxCapacity = 6;

    /// <summary>
    /// Inserts a new node at the specified index in the singly linked list.
    /// </summary>
    /// <param name="index">The zero-based index where the new node should be inserted.</param>
    /// <param name="newNode">The new node to insert into the list.</param>
    /// <remarks>
    /// Adjusts the links between nodes to insert the new node at the desired position.
    /// If the index is beyond the last position, the new node is appended to the end.
    /// </remarks>
    public void InsertAtInstant (int index, SinglyLinkedListNode<T> newNode)
    {
        if (_head == null && index == 0)
        {
            newNode.Next = _head;
            _head = newNode;
            count++;
        }

        SinglyLinkedListNode<T> current = _head;
        int currentIndex = 0;

        while (current.Next != null && currentIndex < index - 1) // -1 to stop at the node before the target index
        {
            current = current.Next;
            currentIndex++;
        }

        newNode.Next = current.Next;
        current.Next = newNode;
        count++;
    }

    /// <summary>
    /// Prepends a new node to the beginning of the singly linked list.
    /// </summary>
    /// <param name="newNode">The new node to prepend to the list.</param>
    public void PrependInstant(SinglyLinkedListNode<T> newNode)
    {
        if (_head == null)
        {
            _head = _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head = newNode;
        }
        count++;
    }

    /// <summary>
    /// Appends a new node to the end of the singly linked list.
    /// </summary>
    /// <param name="newNode">The new node to append to the list.</param>
    public void AppendInstant(SinglyLinkedListNode<T> newNode)
    {
        if (_head == null)
        {
            _head = _tail = newNode;
        }
        else
        {
            _tail.Next = newNode;
            _tail = newNode;
        }
        count++;
    }

    /// <summary>
    /// Deletes a specified node from the singly linked list instantly.
    /// </summary>
    /// <param name="node">The node to delete.</param>
    public void DeleteAtInstant(SinglyLinkedListNode<T> node)
    {
        if (node == null)
        {
            throw new ArgumentNullException(nameof(node), "Node cannot be null.");
        }

        // Case 1: Node is the head
        if (_head == node)
        {
            _head = _head.Next;
            count--;
            return;
        }

        // Traverse the list to find the node
        SinglyLinkedListNode<T> current = _head;

        while (current != null && current.Next != null)
        {
            if (current.Next == node)
            {
                // Node found, bypass it
                current.Next = current.Next.Next;
                count--;
                return;
            }

            current = current.Next;
        }
    }

    /// <summary>
    /// Adds a new node containing the specified data to the end of the singly linked list.
    /// </summary>
    /// <param name="data">The data to store in the new node.</param>
    /// <returns>The newly added node.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the list is at maximum capacity.</exception>
    public SinglyLinkedListNode<T> Add(T data)
    {
        if (count >= maxCapacity)
        {
            throw new InvalidOperationException("List is full.");
        }

        SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);

        if (_head == null)
        {
            _head = _tail = newNode; // If the list is empty, the new node becomes both the _head and the _tail.
        }
        else
        {
            _tail.Next = newNode; // Link the new node to the current _tail.
            _tail = newNode;      // Update the _tail reference to the new node.
        }

        count++; // Increment the node count in the list.
        return newNode;
    }

    /// <summary>
    /// Asynchronously appends a new node to the end of the singly linked list.
    /// </summary>
    /// <param name="newNode">The new node to append to the list.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>The newly appended node.</returns>
    public async Task<SinglyLinkedListNode<T>> AppendAsync(SinglyLinkedListNode<T> newNode, CancellationToken cancellationToken)
    {
        if (HighlightRequested != null)
        {

            // Invoke highlighting for visual effect
            await HighlightRequested.Invoke(AppendSteps.CreateVertex); // "Vertex vtx = new Vertex(v)"
            cancellationToken.ThrowIfCancellationRequested();

            if (_head == null)
            {
                _head = _tail = newNode; // Set both _head and _tail to the new node if list was empty
                await HighlightRequested.Invoke(AppendSteps.UpdateTailNextPointer); // "_tail.next = vtx"
                cancellationToken.ThrowIfCancellationRequested();
            }
            else
            {
                await HighlightRequested.Invoke(AppendSteps.UpdateTailNextPointer); // "_tail.next = vtx"
                cancellationToken.ThrowIfCancellationRequested();
                _tail.Next = newNode;

                _tail = newNode;
                await HighlightRequested.Invoke(AppendSteps.UpdateTail); // "_tail = vtx"
                cancellationToken.ThrowIfCancellationRequested();
            }
        }
        count++;

        return newNode; // Return the newly appended node
    }

    /// <summary>
    /// Asynchronously prepends a new node to the beginning of the singly linked list.
    /// </summary>
    /// <param name="newNode">The new node to prepend to the list.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    public async Task PrependAsync(SinglyLinkedListNode<T> newNode, CancellationToken cancellationToken)
    {
        await HighlightRequested.Invoke(PrependSteps.CreateVertex);
        cancellationToken.ThrowIfCancellationRequested();

        await HighlightRequested.Invoke(PrependSteps.SetNextPointer);
        cancellationToken.ThrowIfCancellationRequested();
        newNode.Next = _head;

        await HighlightRequested.Invoke(PrependSteps.SetHead);
        cancellationToken.ThrowIfCancellationRequested();
        _head = newNode;

        count++;
    }

    /// <summary>
    /// Asynchronously inserts a new node at the specified index in the singly linked list.
    /// </summary>
    /// <param name="index">The zero-based index where the new node should be inserted.</param>
    /// <param name="newNode">The new node to insert into the list.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    public async Task InsertAtAsync(int index, SinglyLinkedListNode<T> newNode, CancellationToken cancellationToken)
    {
        if (_head == null && index == 0)
        {
            newNode.Next = _head;
            _head = newNode;
            count++;
            await HighlightRequested.Invoke(InsertAtPositionSteps.SetPreNextToVertex); // "_head = vtx"
            cancellationToken.ThrowIfCancellationRequested();
        }

        SinglyLinkedListNode<T> current = _head;
        int currentIndex = 0;

        await HighlightRequested.Invoke(InsertAtPositionSteps.InitializePreHead); // "Vertex pre = _head"
        cancellationToken.ThrowIfCancellationRequested();

        // Traverse until the end of the list or the specified index
        while (current.Next != null && currentIndex < index)
        {
            await HighlightRequested.Invoke(InsertAtPositionSteps.LoopToPosition); // "for (k = 0; k<i-1; k++)"
            cancellationToken.ThrowIfCancellationRequested();

            current = current.Next;
            currentIndex++;
            await HighlightRequested.Invoke(InsertAtPositionSteps.MovePreToNext); // "pre = pre.next"
            cancellationToken.ThrowIfCancellationRequested();
        }
        await HighlightRequested.Invoke(InsertAtPositionSteps.SetAftToPreNext); // "Vertex aft = pre.next"
        cancellationToken.ThrowIfCancellationRequested();

        await HighlightRequested.Invoke(InsertAtPositionSteps.CreateVertex); // "Vertex vtx = new Vertex(v)"
        cancellationToken.ThrowIfCancellationRequested();

        await HighlightRequested.Invoke(InsertAtPositionSteps.SetVertexNextToAft); // "vtx.next = aft"
        cancellationToken.ThrowIfCancellationRequested();

        // Insert the new node
        newNode.Next = current.Next;
        current.Next = newNode;
        count++;

        await HighlightRequested.Invoke(InsertAtPositionSteps.SetPreNextToVertex); // "pre.next = vtx"
        cancellationToken.ThrowIfCancellationRequested();
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

    //    HighlightRequested?.Invoke(1); // "Vertex pre = _head"
    //    await Task.Delay(500);

    //    SinglyLinkedListNode<T> current = _head;
    //    if (EqualityComparer<T>.Default.Equals(current._data, data))
    //    {
    //        _head = _head.Next; // Move _head to next node, effectively deleting it
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
    /// Asynchronously deletes the node at the specified index from the singly linked list.
    /// </summary>
    /// <param name="index">The zero-based index of the node to delete.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    public async Task DeleteAtAsync(int index, CancellationToken cancellationToken)
    {

        SinglyLinkedListNode <T> nodeToDelete;
        if (_head == null)
        {
            await HighlightRequested.Invoke(RemoveSteps.CheckIfEmpty); // "if empty, do nothing"
            cancellationToken.ThrowIfCancellationRequested();
            return; // Check if the list is empty and exit
        }

        SinglyLinkedListNode<T> current = _head;

        // Special case for deleting the _head node
        if (index == 0)
        {
            await HighlightRequested.Invoke(RemoveSteps.InitializePreHead); // "Vertex pre = _head"
            cancellationToken.ThrowIfCancellationRequested();

            nodeToDelete = _head;
            _head = _head.Next; // Update the _head to the next node
            nodeToDelete.Next = null; // Explicitly remove reference to the next node
            count--;
            await HighlightRequested.Invoke(RemoveSteps.DeleteDel); // "delete del"
            cancellationToken.ThrowIfCancellationRequested();

            return;
        }

        await HighlightRequested.Invoke(RemoveSteps.InitializePreHead); // "Vertex pre = _head"
        cancellationToken.ThrowIfCancellationRequested();

        // Traverse to the node before the target node
        for (int i = 0; i < index; i++)
        {
            await HighlightRequested.Invoke(RemoveSteps.LoopToPosition); // "for (k = 0; k<i-1; k++)"
            cancellationToken.ThrowIfCancellationRequested();

            current = current.Next;
            await HighlightRequested.Invoke(RemoveSteps.MovePreToNext); // "pre = pre.next"
            cancellationToken.ThrowIfCancellationRequested();
        }

        await HighlightRequested.Invoke(RemoveSteps.SetDelAndAfter); // "Vertex del = pre.next, after = del.next"
        cancellationToken.ThrowIfCancellationRequested();

        // Save the node to be deleted for cleanup
        nodeToDelete = current.Next;

        // Update the link to skip the nodeToDelete
        if (nodeToDelete != null)
        {
            current.Next = nodeToDelete.Next;
            nodeToDelete.Next = null; // Explicitly remove reference to the next node
            count--;
        }

        await HighlightRequested.Invoke(RemoveSteps.UpdatePreNextToAfter); // "pre.next = after"
        cancellationToken.ThrowIfCancellationRequested();

        await HighlightRequested.Invoke(RemoveSteps.DeleteDel); // "delete del"
        cancellationToken.ThrowIfCancellationRequested();
    }


    /// <summary>
    /// Asynchronously searches for a node containing the specified data in the singly linked list.
    /// </summary>
    /// <param name="data">The data to search for.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    public async Task SearchByValueAsync(T data, CancellationToken cancellationToken)
    {
        // Check if the list is empty and handle the first line of the script
        if (_head == null)
        {
            await HighlightRequested.Invoke(SearchSteps.CheckEmptyReturnNotFound); // "return NOT_FOUND."
            cancellationToken.ThrowIfCancellationRequested();
            return;
        }
        await HighlightRequested.Invoke(SearchSteps.InitializeIndexAndHead); // "index = 0, tmp = _head"
        cancellationToken.ThrowIfCancellationRequested();

        SinglyLinkedListNode<T> current = _head;
        int position = 0;

        while (current != null)
        {
            await HighlightRequested.Invoke(SearchSteps.LoopUntilFound); // "while (tmp.item != v)"
            cancellationToken.ThrowIfCancellationRequested();

            if (EqualityComparer<T>.Default.Equals(current._data, data))
            {
                await HighlightRequested.Invoke(SearchSteps.ReturnIndex); // "return index"
                cancellationToken.ThrowIfCancellationRequested();
                return;
            }

            // Update the tmp and index for the next loop iteration
            current = current.Next;
            position++;

            // Check if the next node is null to handle the last if condition
            if (current == null)
            {
                await HighlightRequested.Invoke(SearchSteps.ReturnNull); // "return null"
                cancellationToken.ThrowIfCancellationRequested();
            }
            else
            {
                await HighlightRequested.Invoke(SearchSteps.IncrementIndexAndMoveNext); // "index++, tmp = tmp.next"
                cancellationToken.ThrowIfCancellationRequested();
            }
        }
    }

    /// <summary>
    /// Removes and returns the head node of the singly linked list.
    /// </summary>
    /// <returns>The removed head node.</returns>
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
    /// Finds the node at the specified index in the singly linked list.
    /// </summary>
    /// <param name="index">The zero-based index of the node to find.</param>
    /// <returns>The node at the specified index, or null if not found.</returns>
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
    /// <returns>True if the list is full; otherwise, false.</returns>
    public bool IsFull()
    {
        return count >= maxCapacity;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the nodes in the singly linked list.
    /// </summary>
    /// <returns>An enumerator for the list.</returns>
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