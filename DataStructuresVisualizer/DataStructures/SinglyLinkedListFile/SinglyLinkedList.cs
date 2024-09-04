using DataStructuresVisualizer.DataStructures;
using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
using System.Collections;
using DataStructuresVisualizer.DataStructures.Enums.SinglyLinkedList;
using System.Threading;

public class SinglyLinkedList<T> : IEnumerable<SinglyLinkedListNode<T>>
{
    private SinglyLinkedListNode<T>? _head;
    private SinglyLinkedListNode<T>? _tail;

    public Func<Enum, Task> HighlightRequested;
    public SinglyLinkedListNode<T>? Head => _head;
    public SinglyLinkedListNode<T>? Tail => _tail;

    // Count of nodes in the singly linked list.
    private int count;

    // Maximum capacity of the singly linked list.
    public readonly int maxCapacity = 6;

    // Public property to access the count.
    public int Count => count;

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
    /// Appends a new node to the end of the singly linked list.
    /// </summary>
    /// <param name="newNode">The new node to append to the list.</param>
    /// <returns>The newly appended node.</returns>
    /// <remarks>
    /// This method handles the insertion of a new node at the tail of the list, updating both 
    /// the tail pointer and the links between nodes. It triggers asynchronous highlights to visually 
    /// represent each step of the append operation. Highlighting steps are managed externally, 
    /// allowing for clear separation of business logic from UI visualization concerns.
    /// </remarks>
    public async Task<SinglyLinkedListNode<T>> AppendAsync(SinglyLinkedListNode<T> newNode, CancellationToken cancellationToken)
    {
        if (HighlightRequested != null)
        {

            // Invoke highlighting for visual effect
            await HighlightRequested.Invoke(AppendSteps.CreateVertex); // "Vertex vtx = new Vertex(v)"
            cancellationToken.ThrowIfCancellationRequested();

            if (_head == null)
            {
                _head = _tail = newNode; // Set both head and tail to the new node if list was empty
                await HighlightRequested.Invoke(AppendSteps.UpdateTailNextPointer); // "tail.next = vtx"
                cancellationToken.ThrowIfCancellationRequested();
            }
            else
            {
                await HighlightRequested.Invoke(AppendSteps.UpdateTailNextPointer); // "tail.next = vtx"
                cancellationToken.ThrowIfCancellationRequested();
                _tail.Next = newNode;

                _tail = newNode;
                await HighlightRequested.Invoke(AppendSteps.UpdateTail); // "tail = vtx"
                cancellationToken.ThrowIfCancellationRequested();
            }
        }
        count++;

        return newNode; // Return the newly appended node
    }

    /// <summary>
    /// Prepends a new node to the beginning of the singly linked list.
    /// </summary>
    /// <param name="newNode">The new node to prepend to the list.</param>
    /// <remarks>
    /// This method inserts a new node at the start of the list, updating the head pointer
    /// and linking the new node to the current head. It manages the addition by invoking asynchronous 
    /// highlight operations, which are used for visualizing each step of the prepend process externally.
    /// This separation of business logic from UI allows for clear and maintainable code structure.
    /// </remarks>
    /// <param name="data">The data to prepend to the list.</param>
    public async Task Prepend(SinglyLinkedListNode<T> newNode, CancellationToken cancellationToken)
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
    /// Inserts a new node at the specified index in the singly linked list. 
    /// If the specified index is beyond the last position, the new node is added at the end of the list.
    /// </summary>
    /// <param name="index">The zero-based index at which to insert the new node.</param>
    /// <param name="newNode">The new node to be inserted into the list.</param>
    /// <remarks>
    /// This method navigates to the specified index position, updating the links between nodes to insert 
    /// the new node correctly. If the index matches the start of the list, the new node is set as the head.
    /// The method asynchronously triggers highlight steps for each significant operation, such as node 
    /// creation and pointer updates, allowing the UI to visualize the insertion process. 
    /// </remarks>
    public async Task InsertAt(int index, SinglyLinkedListNode<T> newNode, CancellationToken cancellationToken)
    {
        if (_head == null && index == 0)
        {
            newNode.Next = _head;
            _head = newNode;
            count++;
            await HighlightRequested.Invoke(InsertAtPositionSteps.SetPreNextToVertex); // "head = vtx"
            cancellationToken.ThrowIfCancellationRequested();
        }

        SinglyLinkedListNode<T> current = _head;
        int currentIndex = 0;

        await HighlightRequested.Invoke(InsertAtPositionSteps.InitializePreHead); // "Vertex pre = head"
        cancellationToken.ThrowIfCancellationRequested();

        // Traverse until the end of the list or the specified index
        while (current.Next != null && currentIndex < index - 1) // -1 to stop at the node before the target index
        {
            await HighlightRequested.Invoke(InsertAtPositionSteps.LoopToPosition); // "for (k = 0; k<i-1; k++)"
            cancellationToken.ThrowIfCancellationRequested();

            current = current.Next;
            currentIndex++;
            await HighlightRequested.Invoke(InsertAtPositionSteps.MovePreToNext); // "pre = pre.next"
            cancellationToken.ThrowIfCancellationRequested();


            if (currentIndex == index - 1)
            {
                await HighlightRequested.Invoke(InsertAtPositionSteps.LoopToPosition); // "for (k = 0; k<i-1; k++)"
                cancellationToken.ThrowIfCancellationRequested();
                await HighlightRequested.Invoke(InsertAtPositionSteps.MovePreToNext); // "pre = pre.next"
                cancellationToken.ThrowIfCancellationRequested();

                await HighlightRequested.Invoke(InsertAtPositionSteps.SetAftToPreNext); // "Vertex aft = pre.next"
                cancellationToken.ThrowIfCancellationRequested();

            }
        }

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
    /// Asynchronously deletes the node at the specified index from the singly linked list.
    /// </summary>
    /// <remarks>
    /// This method handles the deletion of nodes from any position within the list, including 
    /// the head, tail, or any node in between. It updates the links between nodes to maintain 
    /// the list structure after deletion. The method also integrates asynchronous highlight steps 
    /// to visually represent the deletion process, aiding in UI visualization without coupling 
    /// business logic with the UI code.
    /// </remarks>
    /// <param name="index">The zero-based index of the node to be deleted.</param>
    public async Task DeleteAt(int index)
    {

        SinglyLinkedListNode <T> nodeToDelete;
        if (_head == null)
        {
            await HighlightRequested.Invoke(RemoveSteps.CheckIfEmpty); // "if empty, do nothing"
            return; // Check if the list is empty and exit
        }

        SinglyLinkedListNode<T> current = _head;

        // Special case for deleting the head node
        if (index == 0)
        {
            await HighlightRequested.Invoke(RemoveSteps.InitializePreHead); // "Vertex pre = head"
            nodeToDelete = _head;
            _head = _head.Next; // Update the head to the next node
            nodeToDelete.Next = null; // Explicitly remove reference to the next node
            count--;
            await HighlightRequested.Invoke(RemoveSteps.DeleteDel); // "delete del"
            return;
        }

        await HighlightRequested.Invoke(RemoveSteps.InitializePreHead); // "Vertex pre = head"

        // Traverse to the node before the target node
        for (int i = 0; i < index - 1; i++)
        {
            await HighlightRequested.Invoke(RemoveSteps.LoopToPosition); // "for (k = 0; k<i-1; k++)"
            current = current.Next;
            await HighlightRequested.Invoke(RemoveSteps.MovePreToNext); // "pre = pre.next"
        }

        await HighlightRequested.Invoke(RemoveSteps.SetDelAndAfter); // "Vertex del = pre.next, after = del.next"

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

        await HighlightRequested.Invoke(RemoveSteps.DeleteDel); // "delete del"
    }


    /// <summary>
    /// Asynchronously searches for the first node containing the specified data in the singly linked list.
    /// </summary>
    /// <remarks>
    /// This method traverses the linked list, starting from the head, to find the first occurrence of the specified data.
    /// It integrates asynchronous highlight steps to visually represent each phase of the search operation, supporting
    /// external visualization and clear separation of concerns between business logic and UI.
    /// </remarks>
    /// <param name="data">The data to search for within the list nodes.</param>
    /// <returns>
    /// The task completes when the search is done, and the highlights are triggered at each step. 
    /// If the data is found, the node is returned via the highlights; otherwise, the search ends, indicating not found.
    /// </returns>
    public async Task SearchByValue(T data, CancellationToken cancellationToken)
    {
        // Check if the list is empty and handle the first line of the script
        if (_head == null)
        {
            await HighlightRequested.Invoke(SearchSteps.CheckEmptyReturnNotFound); // "return NOT_FOUND."
            cancellationToken.ThrowIfCancellationRequested();
            return;
        }
        await HighlightRequested.Invoke(SearchSteps.InitializeIndexAndHead); // "index = 0, tmp = head"
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