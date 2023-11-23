
namespace DataStructuresVisualizer.DataStructures.HashSet
{
    namespace DataStructuresVisualizer.DataStructures
    {
        using System;
        using System.Collections.Generic;

        public class SimpleHashSet<T>
        {
            private LinkedList<T>[] buckets;
            private int capacity;

            public SimpleHashSet(int capacity)
            {
                this.capacity = capacity;
                buckets = new LinkedList<T>[capacity];
                for (int i = 0; i < capacity; i++)
                {
                    buckets[i] = new LinkedList<T>();
                }
            }

            private int GetBucketIndex(T item)
            {
                int hash = item.GetHashCode();
                int index = hash % capacity;
                return Math.Abs(index);
            }

            public void Add(T item)
            {
                int bucketIndex = GetBucketIndex(item);
                LinkedList<T> bucket = buckets[bucketIndex];
                if (!bucket.Contains(item))
                {
                    bucket.AddLast(item);
                }
            }

            public bool Remove(T item)
            {
                int bucketIndex = GetBucketIndex(item);
                return buckets[bucketIndex].Remove(item);
            }

            public bool Contains(T item)
            {
                int bucketIndex = GetBucketIndex(item);
                return buckets[bucketIndex].Contains(item);
            }
        }

    }

}
