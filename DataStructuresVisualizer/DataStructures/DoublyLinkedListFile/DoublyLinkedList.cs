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

        public Action<int, VisualizationTiming> HighlightRequested { get; set; }


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
        public async Task<DoublyLinkedListNode<T>> AppendAsync(T data, VisualizationTiming timing)
        {
            var newNode = new DoublyLinkedListNode<T>(data);

            HighlightRequested?.Invoke(Convert.ToInt32(AppendSteps.CreateVertex), timing);
            await Task.Delay(timing.HighlightDelay);

            if (tail == null)
            {
                head = tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;

                HighlightRequested?.Invoke(Convert.ToInt32(AppendSteps.UpdateTailNextPointer), timing);
                await Task.Delay(timing.HighlightDelay);

                tail = newNode;
            }

            HighlightRequested?.Invoke(Convert.ToInt32(AppendSteps.UpdateTail), timing);
            await Task.Delay(timing.HighlightDelay);

            count++;
            return newNode;
        }

        /// <summary>
        /// Inserts a new node with specified data at a given index.
        /// </summary>
        /// <param name="data">_data for the new node.</param>
        /// <param name="index">Index at which to insert the new node.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is out of bounds.</exception>
        public async Task InsertAtAsync(DoublyLinkedListNode<T> newNode, int index, VisualizationTiming timing)
        {
            var current = head;
            int currentIndex = 0;

            HighlightRequested?.Invoke(Convert.ToInt32(InsertAtPositionSteps.InitializePreHead), timing);
            await Task.Delay(timing.HighlightDelay);

            while (current != null && currentIndex < index)
            {
                HighlightRequested?.Invoke(Convert.ToInt32(InsertAtPositionSteps.LoopToPosition), timing);
                await Task.Delay(timing.HighlightDelay);

                current = current.Next;
                currentIndex++;

                HighlightRequested?.Invoke(Convert.ToInt32(InsertAtPositionSteps.MovePreToNext), timing);
                await Task.Delay(timing.HighlightDelay);
            }
            HighlightRequested?.Invoke(Convert.ToInt32(InsertAtPositionSteps.SetAftToPreNext), timing);
            await Task.Delay(timing.HighlightDelay);

            HighlightRequested?.Invoke(Convert.ToInt32(InsertAtPositionSteps.CreateVertex), timing);
            await Task.Delay(timing.HighlightDelay);

            newNode.Next = current.Next;
            newNode.Prev = current;

            if (current.Next != null)
            {
                current.Next.Prev = newNode;
            }

            HighlightRequested?.Invoke(Convert.ToInt32(InsertAtPositionSteps.SetVertexNextToAft), timing);
            await Task.Delay(timing.HighlightDelay);
            HighlightRequested?.Invoke(Convert.ToInt32(InsertAtPositionSteps.SetPreNextToVertex), timing);
            await Task.Delay(timing.HighlightDelay);

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
        public async Task PrependAsync(DoublyLinkedListNode <T> newNode, VisualizationTiming timing)
        {

            HighlightRequested?.Invoke(Convert.ToInt32(PrependSteps.CreateVertex), timing);
            await Task.Delay(timing.HighlightDelay);

            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                head.Prev = newNode;
                newNode.Next = head;
                HighlightRequested?.Invoke(Convert.ToInt32(PrependSteps.SetPreviousPointer), timing);
                await Task.Delay(timing.HighlightDelay);

                HighlightRequested?.Invoke(Convert.ToInt32(PrependSteps.SetNextPointer), timing);
                await Task.Delay(timing.HighlightDelay);

                head = newNode;
            }

            HighlightRequested?.Invoke(Convert.ToInt32(PrependSteps.SetHead), timing);
            await Task.Delay(timing.HighlightDelay);

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
        public async Task DeleteAtAsync(int index, VisualizationTiming timing)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException($"Index {index} is out of range for the linked list.");
            }

            // Step 1: Check if the list is empty
            if (head == null)
            {
                HighlightRequested?.Invoke(Convert.ToInt32(RemoveSteps.CheckIfEmpty), timing);
                await Task.Delay(timing.HighlightDelay);
                return;
            }

            DoublyLinkedListNode<T> current = head;

            // Step 2: Handle deletion of the head node
            if (index == 0)
            {
                HighlightRequested?.Invoke(Convert.ToInt32(RemoveSteps.InitializePreHead), timing); // Initialize Pre Head
                await Task.Delay(timing.HighlightDelay);

                // Handle deletion of the head node
                if (head.Next != null)
                {
                    head = head.Next;
                    head.Prev = null;
                }
                else
                {
                    // If the list has only one node
                    head = tail = null;
                }

                count--;
                HighlightRequested?.Invoke(Convert.ToInt32(RemoveSteps.DeleteDel), timing); // Delete the node
                await Task.Delay(timing.HighlightDelay);
                return;
            }

            // Step 3: Traverse to the node at the specified index
            for (int i = 0; i < index; i++)
            {
                HighlightRequested?.Invoke(Convert.ToInt32(RemoveSteps.LoopToPosition), timing); // Loop to the position
                await Task.Delay(timing.HighlightDelay);

                current = current.Next;

                HighlightRequested?.Invoke(Convert.ToInt32(RemoveSteps.MovePreToNext), timing); // Move Pre to Next
                await Task.Delay(timing.HighlightDelay);
            }

            // Step 4: Unlink the node from the list
            HighlightRequested?.Invoke(Convert.ToInt32(RemoveSteps.SetDelAndAfter), timing); // Set Del and After
            await Task.Delay(timing.HighlightDelay);

            current.Prev.Next = current.Next;

            if (current.Next != null)
            {
                current.Next.Prev = current.Prev;
            }
            else
            {
                // If the node to be deleted is the tail
                tail = current.Prev;
            }

            // Step 5: Finalize the deletion and update the count
            count--;
            HighlightRequested?.Invoke(Convert.ToInt32(RemoveSteps.DeleteDel), timing); // Delete the node
            await Task.Delay(timing.HighlightDelay);
        }

        /// <summary>
        /// Finds the index of the first occurrence of the specified data in the doubly linked list.
        /// </summary>
        /// <param name="data">The data to locate in the list.</param>
        /// <returns>The zero-based index of the first occurrence of the data, if found; otherwise, -1.</returns>
        public async Task<int> SearchAsync(T data, VisualizationTiming timing)
        {
            int index = 0;
            DoublyLinkedListNode<T> current = head;

            // Step 1: Check if the list is empty
            if (head == null)
            {
                HighlightRequested?.Invoke(Convert.ToInt32(SearchSteps.CheckEmptyReturnNotFound), timing); // "List is empty"
                await Task.Delay(timing.HighlightDelay);
                return -1;
            }

            // Step 2: Traverse the list to find the data
            HighlightRequested?.Invoke(Convert.ToInt32(SearchSteps.InitializeIndexAndHead), timing); // Initialize index and set head
            await Task.Delay(timing.HighlightDelay);

            while (current != null)
            {
                HighlightRequested?.Invoke(Convert.ToInt32(SearchSteps.LoopUntilFound), timing); // Loop until found
                await Task.Delay(timing.HighlightDelay);

                if (EqualityComparer<T>.Default.Equals(current._data, data))
                {
                    HighlightRequested?.Invoke(Convert.ToInt32(SearchSteps.ReturnIndex), timing); // Return index
                    await Task.Delay(timing.HighlightDelay);
                    return index;
                }

                // Move to the next node
                current = current.Next;
                index++;

                // Check if the next node is null
                if (current == null)
                {
                    HighlightRequested?.Invoke(Convert.ToInt32(SearchSteps.CheckIfNullReturnNotFound), timing); // If null, return -1
                    await Task.Delay(timing.HighlightDelay);
                }
                else
                {
                    HighlightRequested?.Invoke(Convert.ToInt32(SearchSteps.IncrementIndexAndMoveNext), timing); // Increment index and move to next
                    await Task.Delay(timing.HighlightDelay);
                }
            }

            HighlightRequested?.Invoke(Convert.ToInt32(SearchSteps.ReturnNull), timing); // Return -1 if not found
            await Task.Delay(timing.HighlightDelay);
            return -1; // Data not found
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