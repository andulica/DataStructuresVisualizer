namespace DataStructuresVisualizer.DataStructures.HashMap
{
    /// <summary>
    /// Represents a key-value pair entry in a data structure.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class Entry<TKey, TValue>
    {
        /// <summary>
        /// Gets or sets the key of the entry.
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Gets or sets the value associated with the key.
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entry{TKey, TValue}"/> class with the specified key and value.
        /// </summary>
        /// <param name="key">The key of the entry.</param>
        /// <param name="value">The value associated with the key.</param>
        public Entry(TKey key, TValue value)
        {
            Key = key;   // Set the key of the entry.
            Value = value; // Set the value associated with the key.
        }
    }
}