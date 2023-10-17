using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;

namespace DataStructuresVisualizer.DataStructures.Stack
{
    public class Stack
    {
        private SinglyLinkedList list;

        public Stack()
        {
            list = new SinglyLinkedList();
        }

        public void Push(int value)
        {
            list.Prepend(value);
        }

        public int? Pop(int k)
        {
            int? lastPopped = null;
            for (int i = 0; i < k; i++)
            {
                lastPopped = list.DeleteHeadForStack();
                if (lastPopped == null)
                {
                    throw new InvalidOperationException("Stack is empty.");
                }
            }
            return lastPopped;
        }

        public int? Peek()
        {
            return list.head?.Data;
        }

        public void PrintStack()
        {
            SinglyLinkedListNode current = list.head;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }
    }
}
