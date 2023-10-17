using DataStructuresVisualizer.DataStructures.Common.Utilities;

namespace DataStructuresVisualizer.DataStructures.DoublyLinkedListFile
{
    public class DoublyLinkedList
    {
        private Node head;
        private Node tail;
        private int count;
        public int Count => count;
        public Node Head => head;
        public Node Tail => tail;

        public DoublyLinkedList()
        {
            Node current = null;

            for (int i = 0; i < 3; i++)  // Create 3 nodes
            {
                Node newNode = new Node(Utilities.randomGenerator.Next(1, 100));

                if (head == null)
                {
                    head = newNode;  // Set head if it's the first node
                }
                else
                {
                    current.Next = newNode;  // Set the next pointer of the previous node
                    newNode.Prev = current;  // Set the previous pointer of the new node
                }

                current = newNode;  // Update current to the new node
                tail = newNode;     // Update tail to the new node
                count++;
            }
        }

        public void Append(int data)
        {
            Node newNode = new Node(data);
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

        public void InsertAt(int data, int index)
        {
            Node newNode = new Node(data);

            if (index == 0)
            {
                Prepend(data); // Use the existing Prepend method for index 0
                return;
            }

            Node current = head;
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

        public void Prepend(int data)
        {
            Node newNode = new Node(data);
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

        public void Delete(int data)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data == data)
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

        public int DeleteHeadForQueue()
        {
            if (Head == null) throw new InvalidOperationException("The list is empty.");

            int value = head.Data;
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

        public bool Search(int data)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data == data) return true;
                current = current.Next;
            }
            return false;
        }

        public override string ToString()
        {
            string result = "";
            Node current = head;
            while (current != null)
            {
                result += current.Data + " <-> ";
                current = current.Next;
            }
            return result + "null";
        }
    }
}
