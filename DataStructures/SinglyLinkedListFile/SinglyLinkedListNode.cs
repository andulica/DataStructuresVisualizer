namespace DataStructuresVisualizer.DataStructures.SinglyLinkedListFile
{
    public class SinglyLinkedListNode<T>
    {
        public T Data { get; set; }
        public SinglyLinkedListNode <T> Next { get; set; }

        public SinglyLinkedListNode(T data)
        {
            this.Data = data;
            Next = null;
        }
    }

    //public class SingleListNodeView
    //{
    //    public bool Highlighted { get; set; }

    //    SinglyLinkedListNode Node { get; set; }

    //}

    //public class ArrowView
    //{
    //    public bool Highlighted { get; set; }
    //    public SingleListNodeView From { get; set; }
    //    public SingleListNodeView To { get; set; }
    //    public bool TwoHeads { get; set; }


    //}
}
