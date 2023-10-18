using DataStructuresVisualizer.DataStructures.DoublyLinkedListFile;

namespace DataStructuresVisualizer.DataStructures
{
    public class Queue
    {
        private DoublyLinkedList list;
        public int Count => list.Count;

        public Queue()
        {
            list = new DoublyLinkedList();
        }

        public void Enqueue(int value)
        {
            list.Append(value);
        }

        public int? Dequeue(int k)
        {
            if (k <= 0 || k > list.Count)
            {
                throw new ArgumentOutOfRangeException("k", "k should be a positive integer and less than or equal to the count of elements in the queue.");
            }

            int? dequeuedValue = null;
            for (int i = 0; i < k; i++)
            {
                dequeuedValue = list.DeleteHeadForQueue();
            }

            return dequeuedValue;
        }

        public int? PeekFront()
        {
            return list.Head?.data;
        }

        public int? PeekBack()
        {
            return list.Tail?.data;
        }

        public void PrintQueue()
        {
            DoublyLinkedListNode current = list.Head;
            while (current != null)
            {
                Console.WriteLine(current.data);
                current = current.Next;
            }
        }
    }
}


