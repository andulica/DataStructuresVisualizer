namespace DataStructuresVisualizer.DataStructures.DoublyLinkedListFile
{
    public class DoublyLinkedListNode<T> : Node<T>
    {
        public DoublyLinkedListNode<T> Next { get; set; }
        public DoublyLinkedListNode<T> Prev { get; set; }



        public DoublyLinkedListNode(T data) : base(data)
        {
            Next = null;
            Prev = null;
        }
    }
}
