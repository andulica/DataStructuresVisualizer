namespace DataStructuresVisualizer.DataStructures.HashSet
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a simple hash set implementation using an array of linked lists.
    /// </summary>
    /// <typeparam name="T">The type of elements in the hash set.</typeparam>
    public class SimpleHashSet<T>
    {
        // Array of linked lists to store the elements of the hash set.
        private LinkedList<T>[] buckets;

        // The capacity of the hash set (number of buckets).
        private int capacity;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleHashSet{T}"/> class with the specified capacity.
        /// </summary>
        /// <param name="capacity">The capacity of the hash set.</param>
        public SimpleHashSet(int capacity)
        {
            this.capacity = capacity;
            buckets = new LinkedList<T>[capacity];
            for (int i = 0; i < capacity; i++)
            {
                buckets[i] = new LinkedList<T>();
            }
        }

        /// <summary>
        /// Computes the bucket index for a given item.
        /// </summary>
        /// <param name="item">The item to compute the index for.</param>
        /// <returns>The index of the bucket.</returns>
        private int GetBucketIndex(T item)
        {
            int hash = item.GetHashCode();
            int index = hash % capacity;
            return Math.Abs(index);
        }

        /// <summary>
        /// Adds an item to the hash set.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item)
        {
            int bucketIndex = GetBucketIndex(item);
            LinkedList<T> bucket = buckets[bucketIndex];
            if (!bucket.Contains(item))
            {
                bucket.AddLast(item);
            }
        }

        /// <summary>
        /// Removes an item from the hash set.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if the item was successfully removed; otherwise, false.</returns>
        public bool Remove(T item)
        {
            int bucketIndex = GetBucketIndex(item);
            return buckets[bucketIndex].Remove(item);
        }

        /// <summary>
        /// Determines whether the hash set contains a specific item.
        /// </summary>
        /// <param name="item">The item to locate in the hash set.</param>
        /// <returns>True if the item is found; otherwise, false.</returns>
        public bool Contains(T item)
        {
            int bucketIndex = GetBucketIndex(item);
            return buckets[bucketIndex].Contains(item);
        }
    }
}