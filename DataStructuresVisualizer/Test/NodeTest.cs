namespace DataStructuresVisualizer.Test
{
    public class NodeTest
    {
        public int Data { get; set; }
        public NodeTest Next { get; set; }

        public NodeTest(int data)
        {
            Data = data;
            Next = null;
        }

        public NodeTest Append(int data)
        {
            NodeTest newNode = new NodeTest(data);
            NodeTest current = this;

            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = newNode;
            return newNode;
        }

        public NodeTest Prepend(int data)
        {
            NodeTest newNode = new NodeTest(data);
            newNode.Next = this;
            return newNode;
        }

        public NodeTest SearchByIndex(int index)
        {
            int currentIndex = 0;
            NodeTest current = this;

            while (current != null && currentIndex < index)
            {
                current = current.Next;
                currentIndex++;
            }

            return current; // This will return null if index is out of bounds
        }

        public void InsertAt(int index, int data)
        {
            if (index == 0)
            {
                NodeTest current = new NodeTest(data);
                current.Next = this;
                // If this is a LinkedList class with Head property, you'd do: Head = newNode.
                // For the current setup, the caller needs to handle this case.
                return;
            }

            NodeTest previous = SearchByIndex(index - 1);
            if (previous == null)
            {
                Console.WriteLine("Index out of bounds");
                return;
            }

            NodeTest newNode = new NodeTest(data);
            newNode.Next = previous.Next;
            previous.Next = newNode;
        }

        public void DeleteAt(int index)
        {
            if (index == 0)
            {
                if (Next != null)
                {
                    Data = Next.Data;
                    Next = Next.Next;
                    return;
                }
                else
                {
                    throw new InvalidOperationException("Can't delete the only node in the list through this method.");
                }
            }

            NodeTest current = this;
            for (int i = 0; i < index - 1; i++)
            {
                if (current.Next == null)
                {
                    throw new IndexOutOfRangeException("Index is out of the range of the list.");
                }
                current = current.Next;
            }

            if (current.Next == null)
            {
                throw new IndexOutOfRangeException("Index is out of the range of the list.");
            }
            current.Next = current.Next.Next;
        }



    }

}
