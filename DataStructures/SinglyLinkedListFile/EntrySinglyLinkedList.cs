using DataStructuresVisualizer.DataStructures.HashMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresVisualizer.DataStructures.SinglyLinkedListFile
{
    public class EntrySinglyLinkedList : IEnumerable
    {
        public EntrySinglyLinkedListNode head { get; set; }
        public void AppendEntry(Entry entry)
        {
            EntrySinglyLinkedListNode newNode = new EntrySinglyLinkedListNode(entry);

            if (head == null)
            {
                head = newNode;
                return;
            }

            EntrySinglyLinkedListNode current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = newNode;
        }

        public void PrependEntry(Entry entry)
        {
            EntrySinglyLinkedListNode newNode = new EntrySinglyLinkedListNode(entry);

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

        public void InsertEntryAt(int index, Entry entry)
        {
            EntrySinglyLinkedListNode newNode = new EntrySinglyLinkedListNode(entry);

            if (index == 0)
            {
                newNode.Next = head;
                head = newNode;
                return;
            }

            EntrySinglyLinkedListNode current = head;
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

        public Entry SearchForEntry(int key)
        {
            EntrySinglyLinkedListNode current = head;
            while (current != null)
            {
                if (current.data.Key == key)
                {
                    return current.data;
                }
                current = current.Next;
            }
            return null;
        }

        public void RemoveEntry(int key)
        {
            if (head == null) return;

            if (head.data.Key == key)
            {
                head = head.Next;
                return;
            }

            EntrySinglyLinkedListNode current = head;
            EntrySinglyLinkedListNode prev = null;
            while (current != null && current.data.Key != key)
            {
                prev = current;
                current = current.Next;
            }

            if (current != null)
            {
                prev.Next = current.Next;
            }
        }

        public bool ContainsKey(int key)
        {
            EntrySinglyLinkedListNode current = head;
            while (current != null)
            {
                if (current.data.Key == key)
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public IEnumerator GetEnumerator()
        {
            EntrySinglyLinkedListNode current = head;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }
    }
}
