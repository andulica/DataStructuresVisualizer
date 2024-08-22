using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;

namespace DataStructuresVisualizer.DataStructures.Stack
{
    public class StackStructure <T> : SinglyLinkedList<T>/*,IEnumerable<T>*/
    {
        // Internal linked list to store the elements of the stack.
        private SinglyLinkedList<T> list = new SinglyLinkedList<T>();
        
        /// <summary>
        /// Adds an item to the top of the stack.
        /// </summary>
        /// <param name="node">The node to add to the stack.</param>
        /// <returns>The node that was added to the stack.</returns>
        public async Task<SinglyLinkedListNode<T>> PushAsync(T value, int delay)
        {
            var nodeToPush = new SinglyLinkedListNode<T>(value);
            return await list.Prepend(nodeToPush, delay);
        }

        /// <summary>
        /// Removes and returns the item at the top of the stack.
        /// </summary>
        /// <returns>The item at the top of the stack.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the stack is empty.</exception>
        public void Pop()
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty.");
            }
        }

        /// <summary>
        /// Returns the item at the top of the stack without removing it.
        /// </summary>
        /// <returns>The item at the top of the stack.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the stack is empty.</exception>
        public void Peek()
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty.");
            }
        }
        /// <summary>
        /// Returns an enumerator that iterates through the stack.
        /// </summary>
        /// <returns>An enumerator for the stack.</returns>
        //public IEnumerator<T> GetEnumerator()
        //{
        //    return list.GetEnumerator();
        //}

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        //{
        //    return list.GetEnumerator();
        //}
    }
}