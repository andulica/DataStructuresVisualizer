using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
using System.Collections.Generic;
using System.Text;

namespace DataStructuresVisualizer.DataStructures.HashMap
{
    public class HashMap<TKey, TValue>
    {
        private readonly SinglyLinkedList<TKey, TValue>[] _buckets;

        public HashMap(int size)
        {
            _buckets = new SinglyLinkedList<TKey, TValue>[size];
            for (int i = 0; i < size; i++)
            {
                _buckets[i] = new SinglyLinkedList<TKey, TValue>();
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

            foreach (var node in bucket)
            {
                if (EqualityComparer<TKey>.Default.Equals(node.Key, key))
                {
                    node.Value = value;
                    return;
                }
            }
            bucket.Append(key, value);
        }

        public TValue Get(TKey key)
        {
            var bucket = _buckets[Hash(key)];
            foreach (var entry in bucket)
            {
                if (EqualityComparer<TKey>.Default.Equals(entry.Key, key))
                {
                    return entry.Value;
                }
            }
            throw new KeyNotFoundException($"The key {key} was not found.");
        }

        public bool Remove(TKey key)
        {
            var bucket = _buckets[Hash(key)];
            return bucket.Remove(key);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("HashMap:");

            for (int i = 0; i < _buckets.Length; i++)
            {
                stringBuilder.Append($"Bucket {i}: ");
                if (_buckets[i].Head == null)
                {
                    stringBuilder.AppendLine("Empty");
                    continue;
                }

                foreach (var entry in _buckets[i])
                {
                    stringBuilder.Append($"[{entry.Key}, {entry.Value}] -> ");
                }
                stringBuilder.AppendLine("null");
            }

            return stringBuilder.ToString();
        }
    }
}

