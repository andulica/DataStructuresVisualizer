using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;

namespace DataStructuresVisualizer.DataStructures.Stack
{
    public class Stack <T>
    {
        private LinkedList<T> list = new LinkedList<T>();

        public void Push(T value)
        {
            list.AddFirst(value);
        }

        public T Pop()
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            T value = list.First.Value;
            list.RemoveFirst();
            return value;
        }

        public T Peek()
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            return list.First.Value;
        }
    }
}
