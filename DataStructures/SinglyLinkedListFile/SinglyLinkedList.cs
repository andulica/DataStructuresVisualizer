using DataStructuresVisualizer.DataStructures.Common.Utilities;

namespace DataStructuresVisualizer.DataStructures.SinglyLinkedListFile
{
    public class SinglyLinkedList<T>
    {
        public SinglyLinkedListNode<T> head { get; set; }



        public void Append(T data)
        {
            SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);

            if (head == null)
            {
                head = newNode;
                return;
            }

            SinglyLinkedListNode<T> current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = newNode;
        }

        public void Prepend(T data)
        {
            SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                newNode.Next = head;
                head = newNode;
            }
        }
        public void InsertAt(int index, T data)
        {
            SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);
            if (index == 0)
            {
                newNode.Next = head;
                head = newNode;
                return;
            }

            SinglyLinkedListNode<T> current = head;
            int currentIndex = 0;
            while (current != null && currentIndex < index - 1)
            {
                current = current.Next;
                currentIndex++;
            }

            if (current == null)
            {
                throw new IndexOutOfRangeException("Index out of range for the linked list.");
            }

            newNode.Next = current.Next;
            current.Next = newNode;
        }

        public void Delete(T data)
        {
            if (head == null) return;

            if (!EqualityComparer<T>.Default.Equals(head.data, data))
            {
                head = head.Next;
                return;
            }

            SinglyLinkedListNode<T> current = head;
            while (current.Next != null && !EqualityComparer<T>.Default.Equals(current.Next.data, data))
            {
                current = current.Next;
            }

            if (current.Next != null)
            {
                current.Next = current.Next.Next;
            }
        }

        public T DeleteHeadForStack()
        {
            if (head == null)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            T value = head.data;
            head = head.Next;
            return value;
        }

        public void DeleteHead()
        {
            if (head == null)
            {
                throw new InvalidOperationException("List is empty.");
            }
            head = head.Next;
        }

        public void DeleteTail()
        {
            if (head == null)
            {
                throw new InvalidOperationException("List is empty.");
            }

            if (head.Next == null)
            {
                head = null;
                return;
            }

            SinglyLinkedListNode<T> current = head;
            while (current.Next.Next != null)
            {
                current = current.Next;
            }
            current.Next = null;
        }

        public T Search(T data)
        {
            SinglyLinkedListNode<T> current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.data, data))
                {
                    return data;
                }
                current = current.Next;
            }

            // You might need a different way to handle not-found cases.
            throw new KeyNotFoundException("Value not found in the list.");
        }

        public override string ToString()
        {
            string result = "";
            SinglyLinkedListNode<T> current = head;
            while (current != null)
            {
                result += current.data + " -> ";
                current = current.Next;
            }
            return result + "null";
        }

        public bool Contains(T value)
        {
            SinglyLinkedListNode<T> current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.data,value))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void AddLast(T value)
        {
            if (head == null)
            {
                head = new SinglyLinkedListNode<T>(value);
                return;
            }

            SinglyLinkedListNode<T> current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = new SinglyLinkedListNode<T>(value);
        }

        public void Remove(T value)
        {
            if (head == null) return;

            if (EqualityComparer<T>.Default.Equals(head.data,value))
            {
                head = head.Next;
                return;
            }

            SinglyLinkedListNode<T> current = head;
            SinglyLinkedListNode<T> prev = null;
            while (current != null && !EqualityComparer<T>.Default.Equals(head.data, value))
            {
                prev = current;
                current = current.Next;
            }

            if (current != null)
            {
                prev.Next = current.Next;
            }
        }
    }
}
