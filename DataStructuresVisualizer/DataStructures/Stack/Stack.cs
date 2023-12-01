namespace DataStructuresVisualizer.DataStructures.Stack
{
    public class Stack <T>
    {
        // Internal linked list to store the elements of the stack.
        private LinkedList<T> list = new LinkedList<T>();

        /// <summary>
        /// Adds an item to the top of the stack.
        /// </summary>
        /// <param name="value">The item to add to the stack.</param>
        public void Push(T value)
        {
            list.AddFirst(value);
        }

        /// <summary>
        /// Removes and returns the item at the top of the stack.
        /// </summary>
        /// <returns>The item at the top of the stack.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the stack is empty.</exception>
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

        /// <summary>
        /// Returns the item at the top of the stack without removing it.
        /// </summary>
        /// <returns>The item at the top of the stack.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the stack is empty.</exception>
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