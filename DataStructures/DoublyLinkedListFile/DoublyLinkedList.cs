using DataStructuresVisualizer.DataStructures.Common.Utilities;

namespace DataStructuresVisualizer.DataStructures.DoublyLinkedListFile
{
    public class DoublyLinkedList<T>
    {
        private DoublyLinkedListNode<T> head;
        private DoublyLinkedListNode<T> tail;
        private int count;
        public int Count => count;
        public DoublyLinkedListNode<T> Head => head;
        public DoublyLinkedListNode<T> Tail => tail;

        

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

        public void Delete(T data)
        {
            DoublyLinkedListNode<T> current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.data,data))
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

        public T DeleteHeadForQueue()
        {
            if (Head == null) throw new InvalidOperationException("The list is empty.");

            T value = head.data;
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

        public bool Search(T data)
        {
            DoublyLinkedListNode <T> current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.data,data)) return true;
                current = current.Next;
            }
            return false;
        }

        public override string ToString()
        {
            string result = "";
            DoublyLinkedListNode<T> current = head;
            while (current != null)
            {
                result += current.data + " <-> ";
                current = current.Next;
            }
            return result + "null";
        }
    }
}
