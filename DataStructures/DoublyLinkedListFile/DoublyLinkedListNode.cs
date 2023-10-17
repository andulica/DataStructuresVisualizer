namespace DataStructuresVisualizer.DataStructures.DoublyLinkedListFile
{
    public class DoublyLinkedListNode : Node
    {
        public DoublyLinkedListNode Next { get; set; }
        public DoublyLinkedListNode Prev { get; set; }



        public DoublyLinkedListNode(int data) : base(data)
        {
            Next = null;
            Prev = null;
        }
    }
}
