﻿namespace DataStructuresVisualizer.DataStructures.SinglyLinkedListFile
{
    public class SinglyLinkedListNode
    {
        public int Data { get; set; }
        public SinglyLinkedListNode Next { get; set; }

        public SinglyLinkedListNode(int data)
        {
            Data = data;
            Next = null;
        }
    }

    public class SingleListNodeView
    {
        public bool Highlighted { get; set; }

        SinglyLinkedListNode Node { get; set; }

    }

    public class ArrowView
    {
        public bool Highlighted { get; set; }
        public SingleListNodeView From { get; set; }
        public SingleListNodeView To { get; set; }
        public bool TwoHeads { get; set; }


    }
}
