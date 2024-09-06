using System.Collections;
using DataStructuresVisualizer.DataStructures.Enums.DoublyLinkedList;
using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;

namespace DataStructuresVisualizer.DataStructures.DoublyLinkedListFile
{
    public class DoublyLinkedList<T> : IEnumerable<DoublyLinkedListNode<T>>
    {
        // Head node of the doubly linked list.
        private DoublyLinkedListNode<T> head;

        // Tail node of the doubly linked list.
        private DoublyLinkedListNode<T> tail;

        public Func<Enum, Task> HighlightRequested;

        // Count of nodes in the doubly linked list.
        private int count;

        // Public property to access the count.
        public int Count => count;

        // Maximum capacity of the doubly linked list.
        public readonly int maxCapacity = 6;

        // Public property to access the head node.
        public DoublyLinkedListNode<T> Head => head;

        // Public property to access the tail node.
        public DoublyLinkedListNode<T> Tail => tail;

        /// <summary>
        /// Appends a new node with the specified data at the end of the list.
        /// </summary>
        /// <param name="data">The data to be appended.</param>
        public DoublyLinkedListNode<T> Append(T data)
        {
            DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(data);
            if (tail == null)
            {
                head = tail = newNode;
                count++;
                return newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
                count++;
            }
            return newNode;
        }

        /// <summary>
        /// Appends a new node with the specified data at the end of the list.
        /// </summary>
        /// <param name="data">The data to be appended.</param>
        public async Task<DoublyLinkedListNode<T>> AppendAsync(T data)
        {
            var newNode = new DoublyLinkedListNode<T>(data);

            await HighlightRequested.Invoke(AppendSteps.CreateVertex); // "Vertex vtx = new Vertex(v)"

            if (tail == null)
            {
                head = tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;

                await HighlightRequested.Invoke(AppendSteps.UpdateTailNextPointer); // "tail.next = vtx"

                tail = newNode;
            }

            await HighlightRequested.Invoke(AppendSteps.UpdateTail);

            count++;
            return newNode;
        }

        /// <summary>
        /// Inserts a new node with specified data at a given index.
        /// </summary>
        /// <param name="data">_data for the new node.</param>
        /// <param name="index">Index at which to insert the new node.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is out of bounds.</exception>
        public async Task InsertAtAsync(DoublyLinkedListNode<T> newNode, int index)
        {
            var current = head;
            int currentIndex = 0;

            await HighlightRequested.Invoke(InsertAtPositionSteps.InitializePreHead);

            while (current != null && currentIndex < index)
            {
                await HighlightRequested.Invoke(InsertAtPositionSteps.LoopToPosition);

                current = current.Next;
                currentIndex++;

                await HighlightRequested.Invoke(InsertAtPositionSteps.MovePreToNext);
            }
            await HighlightRequested.Invoke(InsertAtPositionSteps.SetAftToPreNext);

            await HighlightRequested.Invoke(InsertAtPositionSteps.CreateVertex);

            newNode.Next = current.Next;
            newNode.Prev = current;

            if (current.Next != null)
            {
                current.Next.Prev = newNode;
            }

            await HighlightRequested.Invoke(InsertAtPositionSteps.SetVertexNextToAft);
            await HighlightRequested.Invoke(InsertAtPositionSteps.SetPreNextToVertex);

            current.Next = newNode;

            if (newNode.Next == null)
            {
                tail = newNode;
            }

            count++;
        }

        /// <summary>
        /// Prepends a new node with specified data at the start of the list.
        /// </summary>
        /// <param name="newNode">_data for the new node.</param>
        public async Task PrependAsync(DoublyLinkedListNode <T> newNode)
        {

            await HighlightRequested.Invoke(PrependSteps.CreateVertex);

            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                head.Prev = newNode;
                newNode.Next = head;
                await HighlightRequested.Invoke(PrependSteps.SetPreviousPointer);

                await HighlightRequested.Invoke(PrependSteps.SetNextPointer);

                head = newNode;
            }

            await HighlightRequested.Invoke(PrependSteps.SetHead);

            count++;
        }

        /// <summary>
        /// Deletes a node at the specified index from the doubly linked list.
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
        public async Task DeleteAtAsync(int index)
        {
            if (head == null)
            {
                await HighlightRequested.Invoke(RemoveSteps.CheckIfEmpty);
                return;
            }

            DoublyLinkedListNode<T> current = head;

            if (index == 0)
            {
                await HighlightRequested.Invoke(RemoveSteps.InitializePreHead);

                if (head.Next != null)
                {
                    head = head.Next;
                    head.Prev = null;
                }
                else
                {
                    head = tail = null;
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
                tail = current.Prev;
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
            DoublyLinkedListNode<T> current = head;

            if (head == null)
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

            DoublyLinkedListNode<T> current = head;
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
            DoublyLinkedListNode<T> current = head;
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