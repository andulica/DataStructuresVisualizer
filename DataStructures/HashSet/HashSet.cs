

using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;

namespace DataStructuresVisualizer.DataStructures.HashSet
{
    namespace DataStructuresVisualizer.DataStructures
    {
        public class HashSet
        {
            private SinglyLinkedList[] data;
            private int size;
            private SinglyLinkedList[] buckets;

            public HashSet()
            {
                data = new SinglyLinkedList[10];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = new SinglyLinkedList();
                }
            }

            public bool Search(int value)
            {
                int hash = GetHash(value);
                return data[hash].Search(value) != -1;  // Assumes Search returns -1 if the value is not found
            }

            public void Insert(int value)
            {
                int hash = GetHash(value);
                if (data[hash].Search(value) == -1)  // Check if the value is not already present
                {
                    data[hash].Append(value);  // Assumes Append adds value at the end
                }
            }

            public void Delete(int value)
            {
                int hash = GetHash(value);
                data[hash].Delete(value);  // Assumes Delete removes the value if present
            }

            private int GetHash(int value)
            {
                return value % data.Length;
            }

            public void Print()
            {
                for (int i = 0; i < size; i++)
                {
                    SinglyLinkedList bucket = buckets[i];
                    Console.Write($"Bucket {i}: ");
                    PrintBucket(bucket);
                }
            }

            private void PrintBucket(SinglyLinkedList bucket)
            {
                Node current = bucket.head;  // Assuming head is public or internal, or use a method to get the head

                while (current != null)
                {
                    Console.Write($"{current.Data} -> ");
                    current = current.Next;
                }
                Console.WriteLine("null");
            }
        }
    }

}
