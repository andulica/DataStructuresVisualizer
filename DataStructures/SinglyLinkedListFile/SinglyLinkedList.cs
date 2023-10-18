using DataStructuresVisualizer.DataStructures.Common.Utilities;
using DataStructuresVisualizer.DataStructures.HashMap;
using System.Collections;

namespace DataStructuresVisualizer.DataStructures.SinglyLinkedListFile
{
    public class SinglyLinkedList
    {
        public SinglyLinkedListNode head { get; set; }


        public SinglyLinkedList() //Constructor that builds a linked list with 3 Nodes
        {
            SinglyLinkedListNode current = null;

            for (int i = 0; i < 3; i++)
            {
                SinglyLinkedListNode newNode = new SinglyLinkedListNode(Utilities.randomGenerator.Next(1, 100));

                if (head == null)
                {
                    head = newNode;
                }
                else
                {
                    current.Next = newNode;
                }

                current = newNode;
            }
        }

        public void Append(int data)
        {
            SinglyLinkedListNode newNode = new SinglyLinkedListNode(data);

            if (head == null)
            {
                head = newNode;
                return;
            }

            SinglyLinkedListNode current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = newNode;
        }

        public void Prepend(int data)
        {
            SinglyLinkedListNode newNode = new SinglyLinkedListNode(data);

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
        public void InsertAt(int index, int data)
        {
            SinglyLinkedListNode newNode = new SinglyLinkedListNode(data);
            if (index == 0)
            {
                newNode.Next = head;
                head = newNode;
                return;
            }

            SinglyLinkedListNode current = head;
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

        public void Delete(int data)
        {
            if (head == null) return;

            if (head.data == data)
            {
                head = head.Next;
                return;
            }

            SinglyLinkedListNode current = head;
            while (current.Next != null && current.Next.data != data)
            {
                current = current.Next;
            }

            if (current.Next != null)
            {
                current.Next = current.Next.Next;
            }
        }

        public int DeleteHeadForStack()
        {
            if (head == null)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            int value = head.data;
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

            SinglyLinkedListNode current = head;
            while (current.Next.Next != null)
            {
                current = current.Next;
            }
            current.Next = null;
        }

        public int Search(int data)
        {
            SinglyLinkedListNode current = head;
            while (current != null)
            {
                if (current.data == data)
                {
                    return data;
                }
                current = current.Next;
            }
            return -1;
        }

        public override string ToString()
        {
            string result = "";
            SinglyLinkedListNode current = head;
            while (current != null)
            {
                result += current.data + " -> ";
                current = current.Next;
            }
            return result + "null";
        }

        public bool Contains(int value)
        {
            SinglyLinkedListNode current = head;
            while (current != null)
            {
                if (current.data == value)
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void AddLast(int value)
        {
            if (head == null)
            {
                head = new SinglyLinkedListNode(value);
                return;
            }

            SinglyLinkedListNode current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = new SinglyLinkedListNode(value);
        }

        public void Remove(int value)
        {
            if (head == null) return;

            if (head.data == value)
            {
                head = head.Next;
                return;
            }

            SinglyLinkedListNode current = head;
            SinglyLinkedListNode prev = null;
            while (current != null && current.data != value)
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
