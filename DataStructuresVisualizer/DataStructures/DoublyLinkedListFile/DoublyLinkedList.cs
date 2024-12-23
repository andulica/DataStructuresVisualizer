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
        /// Inserts a node at the specified index in the doubly linked list.
        /// </summary>
        /// <typeparam name="T">The type of the data stored in the nodes.</typeparam>
        /// <param name="node">The node to be inserted.</param>
        /// <param name="index">The zero-based index where the node should be inserted.</param>
        /// <remarks>
        /// If the index is invalid (e.g., greater than the current number of nodes), this method may throw an exception or lead to unexpected behavior.
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
        /// This operation updates the head pointer to point to the new node. If the list is empty, the head and tail pointers will both point to the new node.
        /// </remarks>
        public void PrependInstant(T data)
        {
            DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(data);

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
        /// Appends a new node with the specified data at the end of the list.
        /// </summary>
        /// <param name="data">The data to be appended.</param>
        public DoublyLinkedListNode<T> AppendInstant(T data)
        {
            DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(data);
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
        /// Appends a new node with the specified data at the end of the list.
        /// </summary>
        /// <param name="data">The data to be appended.</param>
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
                node.Prev = _tail;

                await HighlightRequested.Invoke(AppendSteps.UpdateTailNextPointer); // "_tail.next = vtx"
                cancellationToken.ThrowIfCancellationRequested();

                _tail = node;
            }

            await HighlightRequested.Invoke(AppendSteps.UpdateTail);
            cancellationToken.ThrowIfCancellationRequested();

            count++;
            return node;
        }

        /// <summary>
        /// Inserts a new node with specified data at a given index.
        /// </summary>
        /// <param name="data">_data for the new node.</param>
        /// <param name="index">Index at which to insert the new node.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is out of bounds.</exception>
        public async Task InsertAtAsync(DoublyLinkedListNode<T> newNode, int index, CancellationToken cancellationToken)
        {
            var current = _head;
            int currentIndex = 0;

            await HighlightRequested.Invoke(InsertAtPositionSteps.InitializePreHead);
            cancellationToken.ThrowIfCancellationRequested();

            while (current != null && currentIndex < index - 1) // -1 to stop at the node before the target index
            {
                await HighlightRequested.Invoke(InsertAtPositionSteps.LoopToPosition);
                cancellationToken.ThrowIfCancellationRequested();

                current = current.Next;
                currentIndex++;

                await HighlightRequested.Invoke(InsertAtPositionSteps.MovePreToNext);
                cancellationToken.ThrowIfCancellationRequested();
            }
            // current is now the node before the target index so we need to mimic the steps of while loop one more time
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
        /// Prepends a new node with specified data at the start of the list.
        /// </summary>
        /// <param name="newNode">_data for the new node.</param>
        public async Task PrependAsync(DoublyLinkedListNode <T> newNode, CancellationToken cancellationToken)
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
        /// Deletes a node at the specified index from the doubly linked list.
        /// </summary>
        /// <remarks>
        /// This method allows for deletion of nodes at any position within the list.
        /// It can handle deletion of the _head or _tail nodes, as well as any node in between.
        /// The list is zero-indexed.
        /// </remarks>
        /// <param name="index">The zero-based index of the node to be deleted.</param>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown when the specified index is less than 0 or greater than or equal to the size of the list.
        /// </exception>
        public async Task DeleteAtAsync(int index)
        {
            if (_head == null)
            {
                await HighlightRequested.Invoke(RemoveSteps.CheckIfEmpty);
                return;
            }

            DoublyLinkedListNode<T> current = _head;
            await HighlightRequested.Invoke(RemoveSteps.InitializePreHead);

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
                return;
            }

            for (int i = 0; i < index; i++)
            {
                await HighlightRequested.Invoke(RemoveSteps.LoopToPosition);

                current = current.Next;

                await HighlightRequested.Invoke(RemoveSteps.MovePreToNext);
            }

            await HighlightRequested.Invoke(RemoveSteps.SetDelAndAfter);

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
        }

        /// <summary>
        /// Finds the index of the first occurrence of the specified data in the doubly linked list.
        /// </summary>
        /// <param name="data">The data to locate in the list.</param>
        /// <returns>The zero-based index of the first occurrence of the data, if found; otherwise, -1.</returns>
        public async Task SearchAsync(T data)
        {
            int index = 0;
            DoublyLinkedListNode<T> current = _head;

            if (_head == null)
            {
                await HighlightRequested.Invoke(SearchSteps.CheckEmptyReturnNotFound);
                return;
            }

            await HighlightRequested.Invoke(SearchSteps.InitializeIndexAndHead);

            while (current != null)
            {
                await HighlightRequested.Invoke(SearchSteps.LoopUntilFound);

                if (EqualityComparer<T>.Default.Equals(current._data, data))
                {
                    await HighlightRequested.Invoke(SearchSteps.ReturnIndex);
                    return;
                }

                current = current.Next;
                index++;

                if (current == null)
                {
                    await HighlightRequested.Invoke(SearchSteps.CheckIfNullReturnNotFound);
                }
                else
                {
                    await HighlightRequested.Invoke(SearchSteps.IncrementIndexAndMoveNext);
                }
            }

            await HighlightRequested.Invoke(SearchSteps.ReturnNull);
            return;
        }  

        /// <summary>
        /// Determines whether the doubly linked list has reached its maximum capacity.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the count of nodes in the list is greater than or equal to the maximum capacity; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method is used to check if any further insertions are allowed in the doubly linked list.
        /// </remarks>
        public bool IsFull()
        {
            return count >= maxCapacity;
        }

        /// <summary>
        /// Finds the node at the specified index in a doubly linked list.
        /// </summary>
        /// <param name="index">The zero-based index of the node to find.</param>
        /// <returns>The node at the specified index, or null if the index is out of range.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown when the specified index is less than 0 or greater than or equal to the size of the list.
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

            return null; // Should not reach here if the index is valid
        }

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