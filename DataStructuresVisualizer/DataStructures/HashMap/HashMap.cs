using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
using System.Text;

namespace DataStructuresVisualizer.DataStructures.HashMap
{
    /// <summary>
    /// Represents a simple hash map (hash table) implementation using arrays and linked lists.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the hash map.</typeparam>
    /// <typeparam name="TValue">The type of the values in the hash map.</typeparam>
    public class HashMap<TKey, TValue>
    {
        // Array of linked lists to store the hash map's entries.
        private readonly SinglyLinkedList<Entry<TKey, TValue>>[] _buckets;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashMap{TKey, TValue}"/> class with the specified size.
        /// </summary>
        /// <param name="size">The size of the hash map.</param>
        public HashMap(int size)
        {
            _buckets = new SinglyLinkedList<Entry<TKey, TValue>>[size];
            for (int i = 0; i < size; i++)
            {
                _buckets[i] = new SinglyLinkedList<Entry<TKey, TValue>>();
            }
        }

        /// <summary>
        /// Computes the hash for the given key.
        /// </summary>
        /// <param name="key">The key to hash.</param>
        /// <returns>The index in the buckets array.</returns>
        private int Hash(TKey key)
        {
            int hashCode = key.GetHashCode();
            return Math.Abs(hashCode % _buckets.Length);
        }

        /// <summary>
        /// Adds a key-value pair to the hash map.
        /// </summary>
        /// <param name="key">The key to add.</param>
        /// <param name="value">The value associated with the key.</param>
        public void Add(TKey key, TValue value)
        {
            var entry = new Entry<TKey, TValue>(key, value);
            var index = Hash(key);
            var bucket = _buckets[index];

            // Iterate through the nodes in the bucket to see if the key exists
            SinglyLinkedListNode<Entry<TKey, TValue>> current = bucket.Head;
            while (current != null)
            {
                if (EqualityComparer<TKey>.Default.Equals(current._data.Key, key))
                {
                    // Key exists, update the value
                    current._data.Value = value;
                    return;
                }
                current = current.Next;
            }

            // Key does not exist, append new entry
            bucket.Append(entry);
        }

        /// <summary>
        /// Retrieves the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>The value associated with the key.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the key is not found.</exception>
        public TValue GetValue(TKey key)
        {
            var bucket = _buckets[Hash(key)];
            SinglyLinkedListNode<Entry<TKey, TValue>> current = bucket.Head;
            while (current != null)
            {
                if (EqualityComparer<TKey>.Default.Equals(current._data.Key, key))
                {
                    return current._data.Value;
                }
                current = current.Next;
            }
            throw new KeyNotFoundException($"The key {key} was not found.");
        }

        /// <summary>
        /// Removes the entry with the specified key from the hash map.
        /// </summary>
        /// <param name="key">The key of the entry to remove.</param>
        /// <returns>True if the entry was successfully removed; otherwise, false.</returns>
        public bool Remove(TKey key)
        {
            var bucket = _buckets[Hash(key)];
            if (bucket.Head == null) return false;

            if (EqualityComparer<TKey>.Default.Equals(bucket.Head._data.Key, key))
            {
                bucket.Head = bucket.Head.Next;
                return true;
            }

            SinglyLinkedListNode<Entry<TKey, TValue>> current = bucket.Head;
            while (current.Next != null)
            {
                if (EqualityComparer<TKey>.Default.Equals(current.Next._data.Key, key))
                {
                    current.Next = current.Next.Next;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        /// <summary>
        /// Returns a string representation of the hash map.
        /// </summary>
        /// <returns>A string that represents the current hash map.</returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("HashMap:");

            for (int i = 0; i < _buckets.Length; i++)
            {
                stringBuilder.Append($"Bucket {i}: ");
                var current = _buckets[i].Head;
                if (current == null)
                {
                    stringBuilder.AppendLine("Empty");
                    continue;
                }

                while (current != null)
                {
                    stringBuilder.Append($"[{current._data.Key}, {current._data.Value}] -> ");
                    current = current.Next;
                }
                stringBuilder.AppendLine("null");
            }

            return stringBuilder.ToString();
        }
    }
}