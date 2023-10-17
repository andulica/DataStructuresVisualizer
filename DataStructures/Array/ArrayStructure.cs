namespace DataStructuresVisualizer.DataStructures.Array
{
    internal class ArrayStructure
    {

        private int[] data;
        private int size;

        public int length => this.size;

        public ArrayStructure (int capacity)
        {
            this.data = new int[capacity];
            this.size = 0;
        }

        public ArrayStructure() : this(Constants.DEFAULT_ARRAY_CAPACITY)  // Call the existing constructor with a capacity of 3
        {
            Random random = new Random();
            for (int i = 0; i < Constants.DEFAULT_ARRAY_CAPACITY; i++)
            {
                this.Add(random.Next(1, 100));
            }
        }

        public int Get (int index)
        {
            if (index < 0 || index >= this.size)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            return this.data[index];
        }

        public void Set (int index, int value)
        {
            if (index < 0 || index >= this.size)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            this.data[index] = value;
        }

        public void Add(int value)
        {
            if (this.size == this.data.Length)
            {
                throw new InvalidOperationException("Array is full");
            }

            this.data[this.size] = value;
            this.size++;
        }

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

        public void Delete(int value)
        {
            int index = -1;

            for (int i = 0; i < this.size; i++)
            {
                if (this.data[i].Equals(value))
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

        public void PrintArray()
        {
            for (int i = 0; i < this.data.Length - 1; i ++)
            {
                Console.WriteLine(this.data[i]);
            }
        }
    }
}
