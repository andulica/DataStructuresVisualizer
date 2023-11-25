using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
using System.Collections.Generic;
using System.Text;

namespace DataStructuresVisualizer.DataStructures.HashMap
{
    public class HashMap<TKey, TValue>
    {
        private readonly SinglyLinkedList<Entry<TKey, TValue>>[] _buckets;

        public HashMap(int size)
        {
            _buckets = new SinglyLinkedList<Entry<TKey, TValue>>[size];
            for (int i = 0; i < size; i++)
            {
                _buckets[i] = new SinglyLinkedList<Entry<TKey, TValue>>();
            }
        }

        private int Hash(TKey key)
        {
            int hashCode = key.GetHashCode();
            return Math.Abs(hashCode % _buckets.Length);
        }

        public void Add(TKey key, TValue value)
        {
            var entry = new Entry<TKey, TValue>(key, value);
            var index = Hash(key);
            var bucket = _buckets[index];

            // Iterate through the nodes in the bucket to see if the key exists
            SinglyLinkedListNode<Entry<TKey, TValue>> current = bucket.head;
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

        public TValue GetValue(TKey key)
        {
            var bucket = _buckets[Hash(key)];
            SinglyLinkedListNode<Entry<TKey, TValue>> current = bucket.head;
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

        public bool Remove(TKey key)
        {
            var bucket = _buckets[Hash(key)];
            if (bucket.head == null) return false;

            if (EqualityComparer<TKey>.Default.Equals(bucket.head._data.Key, key))
            {
                bucket.head = bucket.head.Next;
                return true;
            }

            SinglyLinkedListNode<Entry<TKey, TValue>> current = bucket.head;
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

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("HashMap:");

            for (int i = 0; i < _buckets.Length; i++)
            {
                stringBuilder.Append($"Bucket {i}: ");
                var current = _buckets[i].head;
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

