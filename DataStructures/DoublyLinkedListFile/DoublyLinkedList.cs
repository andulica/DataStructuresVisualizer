
namespace DataStructuresVisualizer.DataStructures.DoublyLinkedListFile
{
    public class DoublyLinkedList<T>
    {
        // Head node of the doubly linked list.
        private DoublyLinkedListNode<T> head;

        // Tail node of the doubly linked list.
        private DoublyLinkedListNode<T> tail;

        // Count of nodes in the doubly linked list.
        private int count;

        // Public property to access the count.
        public int Count => count;

        // Public property to access the head node.
        public DoublyLinkedListNode<T> Head => head;

        // Public property to access the tail node.
        public DoublyLinkedListNode<T> Tail => tail;

        /// <summary>
        /// Appends a new node with the specified data at the end of the list.
        /// </summary>
        /// <param name="data">The data to be appended.</param>
        public void Append(T data)
        {
            DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(data);
            if (tail == null)
            {
                head = tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
            count++;
        }

        /// <summary>
        /// Inserts a new node with specified data at a given index.
        /// </summary>
        /// <param name="data">Data for the new node.</param>
        /// <param name="index">Index at which to insert the new node.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is out of bounds.</exception>
        public void InsertAt(T data, int index)
        {
            DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(data);

            if (index == 0)
            {
                Prepend(data); // Use the existing Prepend method for index 0
                return;
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
                    return;
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
        /// <param name="data">Data for the new node.</param>
        public void Prepend(T data)
        {
            DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(data);
            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                head.Prev = newNode;
                newNode.Next = head;
                head = newNode;
            }

            count++;
        }

        /// <summary>
        /// Deletes the first node with the specified data from the list.
        /// </summary>
        /// <param name="data">Data of the node to be deleted.</param>
        public void Delete(T data)
        {
            DoublyLinkedListNode<T> current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Data,data))
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

            T value = head.Data;
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
        /// <param name="data">Data to search for.</param>
        /// <returns>True if the data is found, otherwise false.</returns>
        public bool Search(T data)
        {
            DoublyLinkedListNode <T> current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Data,data)) return true;
                current = current.Next;
            }
            return false;
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
                result += current.Data + " <-> ";
                current = current.Next;
            }
            return result + "null";
        }
    }
}
