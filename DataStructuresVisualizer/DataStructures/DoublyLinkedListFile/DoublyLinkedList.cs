using System.Collections;
using System.Threading;

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
        /// Inserts a new node with specified data at a given index.
        /// </summary>
        /// <param name="data">_data for the new node.</param>
        /// <param name="index">Index at which to insert the new node.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is out of bounds.</exception>
        public DoublyLinkedListNode<T> InsertAt(T data, int index)
        {
            DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(data);

            if (index == 0)
            {
                Prepend(data); // Use the existing Prepend method for index 0
                return newNode;
            }

            DoublyLinkedListNode<T> current = head;
            int currentIndex = 0;

            while (current != null)
            {
                if (currentIndex == index - 1)
                {
                    newNode.Next = current.Next;
                    newNode.Prev = current;

                    if (current.Next != null)
                    {
                        current.Next.Prev = newNode;
                    }
                    else
                    {
                        tail = newNode; // If inserting at the end, update the tail
                    }

                    current.Next = newNode;
                    return newNode;
                }

                current = current.Next;
                currentIndex++;
                count++;
            }

            // If we reach here, the index was out of bounds
            throw new IndexOutOfRangeException("Index out of range for the linked list.");
        }

        /// <summary>
        /// Prepends a new node with specified data at the start of the list.
        /// </summary>
        /// <param name="data">_data for the new node.</param>
        public DoublyLinkedListNode<T> Prepend(T data)
        {
            DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(data);
            if (head == null)
            {
                head = tail = newNode;
                count++;
                return newNode;
            }
            else
            {
                head.Prev = newNode;
                newNode.Next = head;
                head = newNode;
                count++;
                return newNode;
            }
        }

        /// <summary>
        /// Deletes the first node with the specified data from the list.
        /// </summary>
        /// <param name="data">_data of the node to be deleted.</param>
        public void Delete(T data)
        {
            DoublyLinkedListNode<T> current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current._data, data))
                {
                    if (current.Prev != null)
                    {
                        current.Prev.Next = current.Next;
                    }
                    else
                    {
                        head = current.Next;
                    }

                    if (current.Next != null)
                    {
                        current.Next.Prev = current.Prev;
                    }
                    else
                    {
                        tail = current.Prev;
                    }
                }
                current = current.Next;
                count--;
            }
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

            DoublyLinkedListNode<T> current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            // Unlink the node from the list
            current.Prev.Next = current.Next;
            if (current.Next != null)
            {
                current.Next.Prev = current.Prev;
            }

            count--;
        }
        /// <summary>
        /// Finds the index of the first occurrence of the specified data in the doubly linked list.
        /// </summary>
        /// <param name="data">The data to locate in the list.</param>
        /// <returns>The zero-based index of the first occurrence of the data, if found; otherwise, -1.</returns>
        public int FindIndexOf(T data)
        {
            int index = 0;
            DoublyLinkedListNode<T> current = head;

            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current._data, data))
                {
                    return index;
                }

                current = current.Next;
                index++;
            }

            return -1; // Data not found
        }
        /// <summary>
        /// Deletes the last node of the list.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the list is empty.</exception>
        public void DeleteTail()
        {
            if (tail == null)
            {
                throw new InvalidOperationException("List is empty.");
            }

            if (tail.Prev != null)
            {
                tail.Prev.Next = null;
            }
            else
            {
                head = null;  // If there's only one node
            }

            tail = tail.Prev;
            count--;
        }

        /// <summary>
        /// Deletes the head node of the list.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the list is empty.</exception>
        public T DeleteHeadForQueue()
        {
            if (Head == null) throw new InvalidOperationException("The list is empty.");

            T value = head._data;
            head = head.Next;
            if (head == null)
            {
                tail = null;
            }
            else
            {
                head.Prev = null;
            }
            count--;  // Decrement count whenever a node is removed
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

            if (head.Next != null)
            {
                head.Next.Prev = null;
            }
            else
            {
                tail = null;  // If there's only one node
            }

            head = head.Next;
            count--;
        }

        /// <summary>
        /// Searches for a node with the specified data.
        /// </summary>
        /// <param name="data">_data to search for.</param>
        /// <returns>True if the data is found, otherwise false.</returns>
        public bool Search(T data)
        {
            DoublyLinkedListNode<T> current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current._data, data)) return true;
                current = current.Next;
            }
            return false;
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

        /// <summary>
        /// Returns a string representation of the doubly linked list.
        /// </summary>
        /// <returns>A string showing all the nodes in the list.</returns>
        public override string ToString()
        {
            string result = "";
            DoublyLinkedListNode<T> current = head;
            while (current != null)
            {
                result += current._data + " <-> ";
                current = current.Next;
            }
            return result + "null";
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