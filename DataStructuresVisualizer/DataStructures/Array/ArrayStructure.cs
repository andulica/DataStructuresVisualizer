using DataStructuresVisualizer.DataStructures.Common.Utilities;

namespace DataStructuresVisualizer.DataStructures.Array
{
    internal class ArrayStructure
    {
        // Holds the array elements.
        private int[] data;

        // Tracks the current number of elements in the array.
        private int size;

        // Property to get the current length of the array.
        public int length => this.size;

        /// <summary>
        /// Constructor to initialize the array with a specific capacity.
        /// </summary>
        /// <param name="capacity">The capacity of the array.</param>
        public ArrayStructure(int capacity)
        {
            this.data = new int[capacity];
            this.size = 0;
        }

        /// <summary>
        /// Default constructor that initializes the array with a default capacity and fills it with random values.
        /// </summary>
        public ArrayStructure() : this(Constants.DEFAULT_ARRAY_CAPACITY)
        {
            for (int i = 0; i < Constants.DEFAULT_ARRAY_CAPACITY; i++)
            {
                this.Add(Utilities.randomGenerator.Next(1, 100));
            }
        }

        /// <summary>
        /// Retrieves the element at the specified index.
        /// </summary>
        /// <param name="index">The index from which to retrieve the element.</param>
        /// <returns>The element at the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of range.</exception>
        public int Get(int index)
        {
            if (index < 0 || index >= this.size)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            return this.data[index];
        }

        /// <summary>
        /// Sets the value of an element at a specified index.
        /// </summary>
        /// <param name="index">The index where the value should be set.</param>
        /// <param name="value">The value to set at the specified index.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of range.</exception>
        public void Set(int index, int value)
        {
            if (index < 0 || index >= this.size)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            this.data[index] = value;
        }

        /// <summary>
        /// Adds a new element to the array.
        /// </summary>
        /// <param name="value">The value to add to the array.</param>
        /// <exception cref="InvalidOperationException">Thrown when the array is full.</exception>
        public void Add(int value)
        {
            if (this.size == this.data.Length)
            {
                throw new InvalidOperationException("Array is full");
            }

            this.data[this.size] = value;
            this.size++;
        }

        /// <summary>
        /// Removes an element at a specified index.
        /// </summary>
        /// <param name="index">The index of the element to remove.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of range.</exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.size)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            for (int i = index; i < this.size - 1; i++)
            {
                this.data[i] = this.data[i + 1];
            }
            this.data[this.size - 1] = default(int);
            this.size--;
        }

        /// <summary>
        /// Deletes the first occurrence of a specified value from the array.
        /// </summary>
        /// <param name="value">The value to delete from the array.</param>
        /// <exception cref="InvalidOperationException">Thrown when the value is not found.</exception>
        public void Delete(int value)
        {
            int index = -1;

            for (int i = 0; i < this.size; i++)
            {
                if (this.data[i] == value)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new InvalidOperationException("Value not found");
            }

            for (int i = index; i < this.size - 1; i++)
            {
                this.data[i] = this.data[i + 1];
            }
            this.data[this.size - 1] = default(int);
            this.size--;
        }

        /// <summary>
        /// Prints the elements of the array to the console.
        /// </summary>
        public void PrintArray()
        {
            for (int i = 0; i < this.size; i++)
            {
                Console.WriteLine(this.data[i]);
            }
        }
    }
}