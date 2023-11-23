namespace DataStructuresVisualizer.DataStructures.DoublyLinkedListFile
{
    public class DoublyLinkedListNode<T>
    {
        public T Data { get; set; }
        public DoublyLinkedListNode<T> Next { get; set; }
        public DoublyLinkedListNode<T> Prev { get; set; }



        public DoublyLinkedListNode(T data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }
    }
}
