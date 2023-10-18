using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;

namespace DataStructuresVisualizer.DataStructures.HashMap
{
    public class HashMap
    {
        private readonly EntrySinglyLinkedList[] _buckets;

        public HashMap(int size)
        {
            _buckets = new EntrySinglyLinkedList[size];
            for (int i = 0; i < size; i++)
            {
                _buckets[i] = new EntrySinglyLinkedList();
            }
        }

        private int Hash(int key)
        {
            return Math.Abs(key) % _buckets.Length;
        }

        public void Add(int key, int value)
        {
            var entry = new Entry(key, value);
            var bucket = _buckets[Hash(key)];

            foreach (EntrySinglyLinkedListNode node in bucket)
            {
                if (node.data.Key == key)
                {
                    node.data.Value = value;
                    return;
                }
            }
            bucket.AppendEntry(entry);
        }

        public int Get(int key)
        {
            var bucket = _buckets[Hash(key)];
            foreach (EntrySinglyLinkedListNode node in bucket)
            {
                if (node.data.Key == key)
                {
                    return (node.data).Value;
                }
            }
            throw new KeyNotFoundException($"The key {key} was not found.");
        }

        public bool Remove(int key)
        {
            var bucket = _buckets[Hash(key)];
            foreach (EntrySinglyLinkedListNode node in bucket)
            {
                if (node.data.Key == key)
                {
                    bucket.RemoveEntry(node.data.Key);
                    return true;
                }
            }
            return false;
        }
    }
}
