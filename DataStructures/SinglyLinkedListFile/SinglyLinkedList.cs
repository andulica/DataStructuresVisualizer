using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresVisualizer.DataStructures.SinglyLinkedListFile
{
    public class SinglyLinkedList
    {
        public Node head;

        public SinglyLinkedList() //Constructor that builds a linked list with 3 Nodes
        {
            Random random = new Random();
            Node current = null;

            for (int i = 0; i < 3; i++)
            {
                Node newNode = new Node(random.Next(1, 100));

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
            Node newNode = new Node(data);

            if (head == null)
            {
                head = newNode;
                return;
            }

            Node current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = newNode;
        }

        public void Prepend(int data)
        {
            Node newNode = new Node(data);

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
            Node newNode = new Node(data);
            if (index == 0)
            {
                newNode.Next = head;
                head = newNode;
                return;
            }

            Node current = head;
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

            if (head.Data == data)
            {
                head = head.Next;
                return;
            }

            Node current = head;
            while (current.Next != null && current.Next.Data != data)
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

            int value = head.Data;
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

            Node current = head;
            while (current.Next.Next != null)
            {
                current = current.Next;
            }
            current.Next = null;
        }

        public int Search(int data)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data == data)
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
            Node current = head;
            while (current != null)
            {
                result += current.Data + " -> ";
                current = current.Next;
            }
            return result + "null";
        }

        public bool Contains(int value)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data == value)
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
                head = new Node(value);
                return;
            }

            Node current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = new Node(value);
        }

        public void Remove(int value)
        {
            if (head == null) return;

            if (head.Data == value)
            {
                head = head.Next;
                return;
            }

            Node current = head;
            Node prev = null;
            while (current != null && current.Data != value)
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
