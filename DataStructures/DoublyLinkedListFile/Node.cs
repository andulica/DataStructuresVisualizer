using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresVisualizer.DataStructures.DoublyLinkedListFile
{
    public class Node
    {
        public int Data { get; set; }
        public Node Next { get; set; }
        public Node Prev { get; set; }



        public Node(int data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }
    }
}
