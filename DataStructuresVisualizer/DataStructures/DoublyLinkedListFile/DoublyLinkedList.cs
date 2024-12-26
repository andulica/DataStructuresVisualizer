using System;
using System.Collections;
using System.Threading;
using DataStructuresVisualizer.DataStructures.Enums.DoublyLinkedList;
using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;

namespace DataStructuresVisualizer.DataStructures.DoublyLinkedListFile
{
    public class DoublyLinkedList<T> : IEnumerable<DoublyLinkedListNode<T>>
    {
        // Head node of the doubly linked list.
        private DoublyLinkedListNode<T> _head;

        // Tail node of the doubly linked list.
        private DoublyLinkedListNode<T> _tail;

        public Func<Enum, Task> HighlightRequested;

        // Count of nodes in the doubly linked list.
        private int count;

        // Public property to access the count.
        public int Count => count;

        // Maximum capacity of the doubly linked list.
        public readonly int maxCapacity = 6;

        // Public property to access the _head node.
        public DoublyLinkedListNode<T> Head => _head;

        // Public property to access the _tail node.
        public DoublyLinkedListNode<T> Tail => _tail;

        /// <summary>
        /// Inserts a node at the specified node in the doubly linked list.
        /// </summary>
        /// <typeparam name="T">The type of the data stored in the nodes.</typeparam>
        /// <param name="node">The node to be inserted.</param>
        /// <param name="index">The zero-based node where the node should be inserted.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the node is less than 0 or greater than the size of the list.</exception>
        /// <remarks>
        /// Adjusts the list's head, tail, and node links as necessary. If the node is invalid, 
        /// the method throws an exception to avoid corrupting the list structure.
        /// </remarks>
        public void InsertAtInstant(DoublyLinkedListNode<T> node, int index)
        {
            var current = _head;
            int currentIndex = 0;

            while (current != null && currentIndex < index - 1)
            {
                current = current.Next;
                currentIndex++;
            }

            node.Next = current.Next;
            node.Prev = current;

            if (current.Next != null)
            {
                current.Next.Prev = node;
            }
            current.Next = node;

            if (node.Next == null)
            {
                _tail = node;
            }

            count++;
        }

        /// <summary>
        /// Prepends a new node containing the specified data to the beginning of the doubly linked list.
        /// </summary>
        /// <typeparam name="T">The type of the data stored in the nodes.</typeparam>
        /// <param name="data">The data to be stored in the new node.</param>
        /// <remarks>
        /// This operation sets the head pointer to the new node. If the list is empty, 
        /// both the head and tail pointers are initialized to point to the new node.
        /// </remarks>
        public void PrependInstant(DoublyLinkedListNode<T> newNode)
        {
            if (_head == null)
            {
                _head = _tail = newNode;
            }
            else
            {
                newNode.Next = _head;
                _head.Prev = newNode;
                _head = newNode;
            }

            count++;
        }

        /// <summary>
        /// Appends a new node with the specified data at the end of the list.
        /// </summary>
        /// <typeparam name="T">The type of the data stored in the nodes.</typeparam>
        /// <param name="data">The data to be appended.</param>
        /// <returns>The newly added node.</returns>
        /// <remarks>
        /// Updates the tail pointer to the newly appended node. If the list is empty, 
        /// initializes both head and tail pointers.
        /// </remarks>
        public DoublyLinkedListNode<T> AppendInstant(DoublyLinkedListNode<T> newNode)
        {
            if (_tail == null)
            {
                _head = _tail = newNode;
                count++;
                return newNode;
            }
            else
            {
                _tail.Next = newNode;
                newNode.Prev = _tail;
                _tail = newNode;
                count++;
            }
            return newNode;
        }

        /// <summary>
        /// Asynchronously appends a node at the end of the doubly linked list.
        /// </summary>
        /// <param name="node">The node to be appended.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>The newly appended node.</returns>
        /// <remarks>
        /// Integrates visual feedback by invoking appropriate steps and supports cancellation.
        /// </remarks>
        public async Task<DoublyLinkedListNode<T>> AppendAsync(DoublyLinkedListNode<T> node, CancellationToken cancellationToken)
        {
            await HighlightRequested.Invoke(AppendSteps.CreateVertex); // "Vertex vtx = new Vertex(v)"
            cancellationToken.ThrowIfCancellationRequested();

            if (_tail == null)
            {
                _head = _tail = node;
            }
            else
            {
                _tail.Next = node;
                await HighlightRequested.Invoke(AppendSteps.UpdateTailNextPointer); // "_tail.next = vtx"
                cancellationToken.ThrowIfCancellationRequested();

                node.Prev = _tail;
                await HighlightRequested.Invoke(AppendSteps.UpdateTailPreviousPointer); // "vtx.prev = _tail"
                cancellationToken.ThrowIfCancellationRequested();

                _tail = node;
                await HighlightRequested.Invoke(AppendSteps.UpdateTail);
                cancellationToken.ThrowIfCancellationRequested();
            }
            count++;
            return node;
        }

        /// <summary>
        /// Asynchronously inserts a node at the specified node in the doubly linked list.
        /// </summary>
        /// <param name="newNode">The node to be inserted.</param>
        /// <param name="index">The zero-based node where the node should be inserted.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <remarks>
        /// This method handles cancellation and provides visual feedback during the operation.
        /// Throws <see cref="IndexOutOfRangeException"/> if the node is invalid.
        /// </remarks>
        public async Task InsertAtAsync(DoublyLinkedListNode<T> newNode, int index, CancellationToken cancellationToken)
        {
            var current = _head;
            int currentIndex = 0;

            await HighlightRequested.Invoke(InsertAtPositionSteps.InitializePreHead);
            cancellationToken.ThrowIfCancellationRequested();

            while (current != null && currentIndex < index - 1) // -1 to stop at the node before the target node
            {
                await HighlightRequested.Invoke(InsertAtPositionSteps.LoopToPosition);
                cancellationToken.ThrowIfCancellationRequested();

                current = current.Next;
                currentIndex++;

                await HighlightRequested.Invoke(InsertAtPositionSteps.MovePreToNext);
                cancellationToken.ThrowIfCancellationRequested();
            }
            // current is now the node before the target node so we need to mimic the steps of while loop one more time
            await HighlightRequested.Invoke(InsertAtPositionSteps.LoopToPosition);
            cancellationToken.ThrowIfCancellationRequested();
            await HighlightRequested.Invoke(InsertAtPositionSteps.MovePreToNext);
            cancellationToken.ThrowIfCancellationRequested();

            await HighlightRequested.Invoke(InsertAtPositionSteps.SetAftToPreNext);
            cancellationToken.ThrowIfCancellationRequested();

            await HighlightRequested.Invoke(InsertAtPositionSteps.CreateVertex);
            cancellationToken.ThrowIfCancellationRequested();

            newNode.Next = current.Next;
            newNode.Prev = current;

            if (current.Next != null)
            {
                current.Next.Prev = newNode;
            }

            await HighlightRequested.Invoke(InsertAtPositionSteps.SetVertexNextToAft);
            cancellationToken.ThrowIfCancellationRequested();

            await HighlightRequested.Invoke(InsertAtPositionSteps.SetPreNextToVertex);
            cancellationToken.ThrowIfCancellationRequested();

            current.Next = newNode;

            if (newNode.Next == null)
            {
                _tail = newNode;
            }

            count++;
        }

        /// <summary>
        /// Asynchronously prepends a node to the beginning of the doubly linked list.
        /// </summary>
        /// <param name="newNode">The node to prepend.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <remarks>
        /// Handles cancellation and provides visual feedback for each operation step.
        /// </remarks>
        public async Task PrependAsync(DoublyLinkedListNode<T> newNode, CancellationToken cancellationToken)
        {

            await HighlightRequested.Invoke(PrependSteps.CreateVertex);
            cancellationToken.ThrowIfCancellationRequested();

            if (_head == null)
            {
                _head = _tail = newNode;
            }
            else
            {
                _head.Prev = newNode;
                newNode.Next = _head;
                await HighlightRequested.Invoke(PrependSteps.SetPreviousPointer);
                cancellationToken.ThrowIfCancellationRequested();

                await HighlightRequested.Invoke(PrependSteps.SetNextPointer);
                cancellationToken.ThrowIfCancellationRequested();

                _head = newNode;
            }

            await HighlightRequested.Invoke(PrependSteps.SetHead);
            cancellationToken.ThrowIfCancellationRequested();

            count++;
        }

        /// <summary>
        /// Deletes a node at the specified node from the doubly linked list asynchronously.
        /// </summary>
        /// <param name="index">The zero-based node of the node to delete.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <remarks>
        /// Supports cancellation and visual feedback. Adjusts the list pointers and decreases the node count.
        /// Throws <see cref="IndexOutOfRangeException"/> if the node is invalid.
        /// </remarks>
        public async Task DeleteAtAsync(int index, CancellationToken cancellationToken)
        {
            if (_head == null)
            {
                await HighlightRequested.Invoke(RemoveSteps.CheckIfEmpty);
                cancellationToken.ThrowIfCancellationRequested();
                return;
            }

            DoublyLinkedListNode<T> current = _head;
            await HighlightRequested.Invoke(RemoveSteps.InitializePreHead);
            cancellationToken.ThrowIfCancellationRequested();

            if (index == 0)
            {

                if (_head.Next != null)
                {
                    _head = _head.Next;
                    _head.Prev = null;
                }
                else
                {
                    _head = _tail = null;
                }

                count--;
                await HighlightRequested.Invoke(RemoveSteps.DeleteDel);
                cancellationToken.ThrowIfCancellationRequested();
                return;
            }

            for (int i = 0; i < index; i++)
            {
                await HighlightRequested.Invoke(RemoveSteps.LoopToPosition);
                cancellationToken.ThrowIfCancellationRequested();

                current = current.Next;

                await HighlightRequested.Invoke(RemoveSteps.MovePreToNext);
                cancellationToken.ThrowIfCancellationRequested();
            }

            await HighlightRequested.Invoke(RemoveSteps.SetDelAndAfter);
            cancellationToken.ThrowIfCancellationRequested();

            current.Prev.Next = current.Next;

            if (current.Next != null)
            {
                current.Next.Prev = current.Prev;
            }
            else
            {
                _tail = current.Prev;
            }

            count--;
            await HighlightRequested.Invoke(RemoveSteps.DeleteDel);
            cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>
        /// Deletes a specified node in the doubly linked list instantly.
        /// </summary>
        /// <param name="node">The node to delete.</param>
        public void DeleteAtInstant(DoublyLinkedListNode<T> node)
        {

            // Case 1: Node is the head
            if (node == _head)
            {
                _head = _head.Next;
                if (_head != null)
                {
                    _head.Prev = null;
                }
                else
                {
                    // If the list is now empty, update the tail as well
                    _tail = null;
                }
                count--;
                return;
            }

            // Case 2: Node is the tail
            if (node == _tail)
            {
                _tail = _tail.Prev;
                if (_tail != null)
                {
                    _tail.Next = null;
                }
                count--;
                return;
            }

            // Case 3: Node is in the middle
            if (node.Prev != null)
            {
                node.Prev.Next = node.Next;
            }

            if (node.Next != null)
            {
                node.Next.Prev = node.Prev;
            }

            count--;
        }

        /// <summary>
        /// Asynchronously searches for the first occurrence of the specified data in the doubly linked list.
        /// </summary>
        /// <param name="data">The data to locate.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>The node of the data if found; otherwise, -1.</returns>
        /// <remarks>
        /// Provides visual feedback during the search and supports cancellation.
        /// </remarks>
        public async Task SearchAsync(T data, CancellationToken cancellationToken)
        {
            int index = 0;
            DoublyLinkedListNode<T> current = _head;

            if (_head == null)
            {
                await HighlightRequested.Invoke(SearchSteps.CheckEmptyReturnNotFound);
                cancellationToken.ThrowIfCancellationRequested();
                return;
            }

            await HighlightRequested.Invoke(SearchSteps.InitializeIndexAndHead);
            cancellationToken.ThrowIfCancellationRequested();

            while (current != null)
            {
                await HighlightRequested.Invoke(SearchSteps.LoopUntilFound);
                cancellationToken.ThrowIfCancellationRequested();

                if (EqualityComparer<T>.Default.Equals(current._data, data))
                {
                    await HighlightRequested.Invoke(SearchSteps.ReturnIndex);
                    cancellationToken.ThrowIfCancellationRequested();
                    return;
                }

                current = current.Next;
                index++;

                if (current == null)
                {
                    await HighlightRequested.Invoke(SearchSteps.CheckIfNullReturnNotFound);
                    cancellationToken.ThrowIfCancellationRequested();
                }
                else
                {
                    await HighlightRequested.Invoke(SearchSteps.IncrementIndexAndMoveNext);
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }

            await HighlightRequested.Invoke(SearchSteps.ReturnNull);
            cancellationToken.ThrowIfCancellationRequested();
            return;
        }

        /// <summary>
        /// Checks if the doubly linked list has reached its maximum capacity.
        /// </summary>
        /// <returns><c>true</c> if the list is full; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// Used to enforce constraints on the list's size.
        /// </remarks>
        public bool IsFull()
        {
            return count >= maxCapacity;
        }

        /// <summary>
        /// Adds a new node containing the specified data to the end of the doubly linked list.
        /// </summary>
        /// <param name="data">The data to store in the new node.</param>
        /// <returns>The newly added node.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the list is at maximum capacity.</exception>
        public DoublyLinkedListNode<T> Add(T data)
        {
            if (count >= maxCapacity)
            {
                throw new InvalidOperationException("List is full.");
            }

            DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(data);

            if (_head == null)
            {
                _head = _tail = newNode; // If the list is empty, the new node becomes both the _head and the _tail.
            }
            else
            {
                _tail.Next = newNode; // Link the new node to the current _tail.
                newNode.Prev = _tail;
                _tail = newNode;      // Update the _tail reference to the new node.
            }

            count++; // Increment the node count in the list.
            return newNode;
        }


        /// <summary>
        /// Finds the node at the specified node in the doubly linked list.
        /// </summary>
        /// <param name="index">The zero-based node of the node to locate.</param>
        /// <returns>The node at the specified node, or <c>null</c> if the node is invalid.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if the node is less than 0 or greater than or equal to the size of the list.
        /// </exception>
        public DoublyLinkedListNode<T> FindNodeByIndex(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException($"Index {index} is out of range for the linked list.");
            }

            DoublyLinkedListNode<T> current = _head;
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

            return null; // Should not reach here if the node is valid
        }

        /// <summary>
        /// Returns an enumerator that iterates through the nodes in the doubly linked list.
        /// </summary>
        /// <returns>An enumerator for the list.</returns
        public IEnumerator<DoublyLinkedListNode<T>> GetEnumerator()
        {
            DoublyLinkedListNode<T> current = _head;
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
}