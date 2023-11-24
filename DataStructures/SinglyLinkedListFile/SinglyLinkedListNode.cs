namespace DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
    using DataStructuresVisualizer.DataStructures.HashMap;

public class SinglyLinkedListNode<TKey, TValue>
{
    public Entry<TKey, TValue> Data { get; set; }
    public SinglyLinkedListNode<TKey, TValue> Next { get; set; }

    public SinglyLinkedListNode(Entry<TKey, TValue> data)
    {
        Data = data;
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

